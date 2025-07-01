namespace API_SerenityShield.Models.DAO
{
    public class PersonnalStrongboxInsertModel
    {

        public string label { get; set; }
        public string scPK { get; set; }
        public string codeID { get; set; }
        public string payingPK { get; set; }
        public string secretPK { get; set; }
        public string solanaPK { get; set; }
        public string solUsrNftPK { get; set; }
        public string solSereNftPK { get; set; }
        public string solHeirNftPKs
        {
            get; set;

        }

        public List<IWallet> content { get; set; }

    }
    public class IWallet
    {

        public string label { get; set; }
        public string type { get; set; }
        public string provider { get; set; }

        // public string? idtwofa { get; set; }
        public string idTypeWallet { get; set; }
        public string? seed { get; set; }
        public IWallet()
        { }
    }
    public class PersonnalStrongboxUpdateModel
    {

        public string label { get; set; }
        public string walletPublicKeyOwner { get; set; }
        public string scPK { get; set; }
        public string codeID { get; set; }
        public string payingPK { get; set; }
        public string secretPK { get; set; }
        public string solanaPK { get; set; }
        public string solUsrNftPK { get; set; }
        public string solSereNftPK { get; set; }
        public string solHeirNftPKs
        {
            get; set;

        }

        public string id { get; set; }
        public List<IWallet> content { get; set; }

    }
    public class PersonnalStrongboxDeleteModel
    {


        public string idPersonnalStrongbox { get; set; }
        public List<IWallet> content { get; set; }
    }
    public class StrongboxForHeirInsertModel
    {

        public string idInactivePeriod { get; set; }

        public string label { get; set; }
        public string messageForHeirs { get; set; }
        public bool display { get; set; }
        
        public bool prepayedInherence { get; set; }
        public string scPK { get; set; }
        public string codeID { get; set; }
        public string payingPK { get; set; }
        public string secretPK { get; set; }
        public string solanaPK { get; set; }
        public string solUsrNftPK { get; set; }
        public string solSereNftPK { get; set; }
        public string solHeirNftPKs
        {
            get; set;

        }
        public List<IWallet> content
        {
            get; set;
        }
        public List<IHeir> heirs { get; set; }
    }
    public class StrongboxForHeirUpdateModel
    {
        public string id { get; set; }
        public string label { get; set; }
        public string walletPublicKeyOwner { get; set; }
        public string scPK { get; set; }
        public string codeID { get; set; }
        public string payingPK { get; set; }
        public string secretPK { get; set; }
        public string solanaPK { get; set; }
        public string solUsrNftPK { get; set; }
        public string solSereNftPK { get; set; }
        public string solHeirNftPKs
        {
            get; set;

        }

        public string idInactivePeriod { get; set; }

        public string messageForHeirs { get; set; }
        public bool display { get; set; }

        public bool prepayedInherence { get; set; }

        public List<IWallet> content { get; set; }

        public List<IHeir> heirs { get; set; }
    }
    public class StrongboxForHeirDeleteModel
    {

        public string idStrongboxforHeir { get; set; }

    }
    public class ContentwalletDexInsertModel
    {
        public bool personnal { get; set; }
        public string idStrongbox { get; set; }

        public string label { get; set; }
        public string idTypeWalletDex { get; set; }
        public string seed { get; set; }

        public string provider { get; set; }
    }
    public class ContentwalletDexUpdateModel
    {

        public string label { get; set; }


        public string idContentwalletDex { get; set; }

    }
    public class ContentwalletDexDeleteModel
    {

        public string idContentwalletDex { get; set; }

    }
    public class ContentwalletCexInsertModel
    {
        public bool personnal { get; set; }
        public string idStrongbox { get; set; }

        public string label { get; set; }
        public string idTypeWalletCex { get; set; }
        public string provider { get; set; }

    }
    public class ContentwalletCexUpdateModel
    {

        public string label { get; set; }


        public string idContentwalletCex { get; set; }

    }
    public class ContentwalletCexDeleteModel
    {

        public string idContentwalletCex { get; set; }

    }
    public class ContentwalletMobileInsertModel
    {
        public bool personnal { get; set; }
        public string idStrongbox { get; set; }
        // public string idtwofa { get; set; }
        public string label { get; set; }
        public string idTypeWalletMobile { get; set; }
        public string provider { get; set; }

    }
    public class ContentwalletMobileUpdateModel
    {

        public string label { get; set; }


        public string idContentwalletMobile { get; set; }

    }
    public class ContentwalletMobileDeleteModel
    {

        public string idContentwalletMobile { get; set; }

    }
    public class ContentwalletDesktopInsertModel
    {
        public bool personnal { get; set; }
        public string idStrongbox { get; set; }

        public string label { get; set; }
        public string idTypeWalletDesktop { get; set; }
        public string provider { get; set; }

    }
    public class ContentwalletDesktopUpdateModel
    {

        public string label { get; set; }


        public string idContentwalletDesktop { get; set; }

    }
    public class ContentwalletDesktopDeleteModel
    {

        public string idContentwalletDesktop { get; set; }

    }
    public class ContentwalletHardwareInsertModel
    {
        public bool personnal { get; set; }
        public string idStrongbox { get; set; }
        public string label { get; set; }
        public string idTypeWalletHardware { get; set; }
        public string provider { get; set; }

    }
    public class ContentwalletHardwareUpdateModel
    {

        public string label { get; set; }


        public string idContentwalletHardware { get; set; }

    }
    public class ContentwalletHardwareDeleteModel
    {

        public string idContentwalletHardware { get; set; }

    }

}

