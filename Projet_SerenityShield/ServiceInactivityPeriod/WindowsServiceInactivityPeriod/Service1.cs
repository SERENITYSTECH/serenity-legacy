using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using ServiceInactivityPeriod.AppCode;

namespace ServiceInactivityPeriod
{
    public partial class Service1 : ServiceBase
    {
        public void onDebug()
        {
            OnStart(null);
        }
        public Service1()
        {
            InitializeComponent();
        }
        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }

        protected override void OnStart(string[] args)
        {

            this.WriteToFile("ServiceInactivityPeriode started {0}");
            Console.WriteLine("ServiceInactivityPeriode started {0}");
            this.ScheduleService();
        }

        protected override void OnStop()
        {
            this.WriteToFile("ServiceInactivityPeriode stopped {0}");
            Console.WriteLine("ServiceInactivityPeriode stopped {0}");
            this.Schedular.Dispose();
        }
        private System.Threading.Timer Schedular;

        public void ScheduleService()
        {
            try
            {
                Schedular = new Timer(new TimerCallback(SchedularCallback));
                string mode = ConfigurationManager.AppSettings["Mode"].ToUpper();
                this.WriteToFile("ServiceInactivityPeriode Mode: " + mode + " {0}");
                Console.WriteLine("ServiceInactivityPeriode Mode: " + mode + " {0}");
                //Set the Default Time.
                DateTime scheduledTime = DateTime.MinValue;

                if (mode == "DAILY")
                {
                    //Get the Scheduled Time from AppSettings.
                    scheduledTime = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["ScheduledTime"]);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next day.
                        scheduledTime = scheduledTime.AddDays(1);
                    }
                }

                if (mode.ToUpper() == "INTERVAL")
                {
                    //Get the Interval in Minutes from AppSettings.
                    int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);

                    //Set the Scheduled Time by adding the Interval to Current Time.
                    scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next Interval.
                        scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                    }
                }

                TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now);
                string schedule = string.Format("{0} day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                this.WriteToFile("ServiceInactivityPeriode scheduled to run after: " + schedule + " {0}");
                Console.WriteLine("ServiceInactivityPeriode scheduled to run after: " + schedule + " {0}");
                //ecrire ici le code d'envoi de mail

                CheckActivity();



                //Get the difference in Minutes between the Scheduled and Current Time.
                int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

                //Change the Timer's Due Time.
                Schedular.Change(dueTime, Timeout.Infinite);

            }
            catch (Exception ex)
            {
                WriteToFile("ServiceInactivityPeriode Error on: {0} " + ex.Message + ex.StackTrace);
                Console.WriteLine("ServiceInactivityPeriode Error on: {0} " + ex.Message + ex.StackTrace);
                //Stop the Windows Service.
                using (System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController("ServiceInactivityPeriode"))
                {
                    serviceController.Stop();
                }
            }
        }

        public void CheckActivity()
        {
            List<IStrongboxForHeirs> laliste = new List<IStrongboxForHeirs>();
            IStrongboxForHeirs sb = new IStrongboxForHeirs();
            laliste = sb.GetListStrongboxForHeir();
            string date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            foreach (IStrongboxForHeirs item in laliste)
            {
                string idOwner = item.idOwner;
                string idInactivityperiod = item.idInactivityPeriod;

                Customer customer = new Customer();
                customer = customer.GetCustomerById(idOwner);

                string idUser = customer.IdUser;
                User user = new User();
                user=user.GetUserById(idUser);
                SendEmail mail = new SendEmail();
          
                //Check all case from HistoriqueUser
                List<HistoriqueUser> lalisteHu = new List<HistoriqueUser>();
                HistoriqueUser hu = new HistoriqueUser();
                lalisteHu = hu.GethistoriqueUser(idUser);
                //get last action date from historique User
                DateTime lastDateAction = DateTime.Parse(lalisteHu[0].Date_creation);

                //Check mail Sended
                ActivityVerification av = new ActivityVerification();
                string curentId = string.Empty;
                bool mailSended = false;
                mailSended = av.IfEmailSended(idUser, item.id);
                Cryptage cr=new Cryptage(); 
                switch (idInactivityperiod)
                {
                    case "1":
                        if (DateTime.Now.AddDays(-7) > lastDateAction)
                        {

                            if (mailSended == true)
                            {
                                av = av.GetActivityVerificationByIdUserandIdSbForHeir(idUser, item.id);
                                if (av.Active == "True")
                                {

                                    DateTime dateSendMail = DateTime.Parse(av.Date_Email);
                                    int diff = (DateTime.Now - dateSendMail).Days;
                                    if (diff > 7)
                                    {
                                        string resultat = av.DesactiveActivityVerification(idUser, item.id);
                                        mail.SendMailToServiceInactivity(idUser, item);
                                        this.WriteToFile("Mail Send to policy-activity@serenityshield.io "+date+" concerning userId"+idUser+" lastname:"+user.LastName+" firstname:"+user.FirstName + " StrongboxId:" + item.id);
                                        Console.WriteLine("Mail Send to policy-activity@serenityshield.io " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName+" StrongboxId:"+item.id);
                                    }
                                }
                            }
                            else
                            {
                                curentId = av.InsertActivityVerification(idUser, item.id, idInactivityperiod);
                                mail.SendMailInactivity(customer.Email, idUser, item.id);
                                this.WriteToFile("Mail Send to " + user.Email + " " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                                Console.WriteLine("Mail Send to " + user.Email + " " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                            }

                        }
                        break;
                    case "2":
                        if (DateTime.Now.AddDays(-15) > lastDateAction)
                        {
                            if (mailSended == true)
                            {
                                av = av.GetActivityVerificationByIdUserandIdSbForHeir(idUser, item.id);
                                if (av.Active == "True")
                                {
                                    DateTime dateSendMail = DateTime.Parse(av.Date_Email);
                                    int diff = (DateTime.Now - dateSendMail).Days;
                                    if (diff > 7)
                                    {
                                        string resultat = av.DesactiveActivityVerification(idUser, item.id);
                                        mail.SendMailToServiceInactivity(idUser, item);
                                        this.WriteToFile("Mail Send to policy-activity@serenityshield.io " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                                        Console.WriteLine("Mail Send to policy-activity@serenityshield.io " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                                    }
                                }
                            }
                            else
                            {
                                curentId = av.InsertActivityVerification(idUser, item.id, idInactivityperiod);
                                mail.SendMailInactivity(customer.Email, idUser, item.id);
                                this.WriteToFile("Mail Send to " + user.Email + " " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                                Console.WriteLine("Mail Send to " + user.Email + " " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                            }
                        }
                        break;
                    case "3":
                        if (DateTime.Now.AddMonths(-1) > lastDateAction)
                        {
                            if (mailSended == true)
                            {
                                av = av.GetActivityVerificationByIdUserandIdSbForHeir(idUser, item.id);
                                if (av.Active == "True")
                                {
                                    DateTime dateSendMail = DateTime.Parse(av.Date_Email);
                                    int diff = (DateTime.Now - dateSendMail).Days;
                                    if (diff > 7)
                                    {
                                        string resultat = av.DesactiveActivityVerification(idUser, item.id);
                                        mail.SendMailToServiceInactivity(idUser, item);
                                        this.WriteToFile("Mail Send to policy-activity@serenityshield.io " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                                        Console.WriteLine("Mail Send to policy-activity@serenityshield.io " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                                    }
                                }
                            }
                            else
                            {
                                curentId = av.InsertActivityVerification(idUser, item.id, idInactivityperiod);
                                mail.SendMailInactivity(customer.Email, idUser, item.id);
                                this.WriteToFile("Mail Send to " + user.Email + " " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                                Console.WriteLine("Mail Send to " + user.Email + " " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                            }
                        }
                        break;
                    case "4":
                        if (DateTime.Now.AddMonths(-1) > lastDateAction)
                        {

                            if (mailSended == true)
                            {
                                av = av.GetActivityVerificationByIdUserandIdSbForHeir(idUser, item.id);
                                if (av.Active == "True")
                                {
                                    DateTime dateSendMail = DateTime.Parse(av.Date_Email);
                                    int diff = (DateTime.Now - dateSendMail).Days;
                                    if (diff > 7)
                                    {
                                        string resultat = av.DesactiveActivityVerification(idUser, item.id);
                                        mail.SendMailToServiceInactivity(idUser, item);
                                        this.WriteToFile("Mail Send to policy-activity@serenityshield.io " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                                        Console.WriteLine("Mail Send to policy-activity@serenityshield.io " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                                    }
                                }
                            }
                            else
                            {
                                curentId = av.InsertActivityVerification(idUser, item.id, idInactivityperiod);
                                mail.SendMailInactivity(customer.Email, idUser, item.id);
                                this.WriteToFile("Mail Send to "+ user.Email+" " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                                Console.WriteLine("Mail Send to " + user.Email + " " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                            }
                        }
                        break;
                    case "5"://case deceaded


                        av = av.GetActivityVerificationByIdUserandIdSbForHeir(idUser, item.id);
                        if (DateTime.Now.AddMonths(-2) > DateTime.Parse(av.Date_Death) && av.Active=="False")
                        {
                            mail.SendMailToServiceInactivityToUnlockStrongBoxForHeir(idUser, item);
                            this.WriteToFile("Mail Send to policy-activity@serenityshield.io to unlock strongbox for heir " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                            Console.WriteLine("Mail Send to policy-activity@serenityshield.io to unlock strongbox for heir " + date + " concerning userId" + idUser + " lastname:" + user.LastName + " firstname:" + user.FirstName + " StrongboxId:" + item.id);
                        }

                        break;
                    default:
                        break;

                }
            
            }
        }

        private void SchedularCallback(object e)
        {
            this.WriteToFile("ServiceInactivityPeriode Log: {0}");
            Console.WriteLine("ServiceInactivityPeriode Log: {0}");
            this.ScheduleService();
        }

        private void WriteToFile(string text)
        {
            string now = DateTime.Now.ToString("dd-MM-yyyy");
            string path = @"C:\ServiceInactivityPeriod\ServiceInactivityPeriodLog_" + now + ".txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                Console.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }
     

    }
}
