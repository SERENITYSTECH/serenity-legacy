/*==============================================================*/
/* Nom de SGBD :  Microsoft SQL Server 2000                     */
/* Date de création :  13/09/2022 15:58:12                      */
/*==============================================================*/


IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('ACTIVITY_VERIFICATION') AND O.NAME = 'FK_ACTIVITY_NEED_STRO_STRONGBO')
ALTER TABLE ACTIVITY_VERIFICATION
   DROP CONSTRAINT FK_ACTIVITY_NEED_STRO_STRONGBO
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('ACTIVITY_VERIFICATION') AND O.NAME = 'FK_ACTIVITY_NEED_USER__USER')
ALTER TABLE ACTIVITY_VERIFICATION
   DROP CONSTRAINT FK_ACTIVITY_NEED_USER__USER
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('AFFILIATE') AND O.NAME = 'FK_AFFILIAT_AFFILIATE_CUSTOMER')
ALTER TABLE AFFILIATE
   DROP CONSTRAINT FK_AFFILIAT_AFFILIATE_CUSTOMER
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('AFFILIATE') AND O.NAME = 'FK_AFFILIAT_AFFILIATE_HEIR')
ALTER TABLE AFFILIATE
   DROP CONSTRAINT FK_AFFILIAT_AFFILIATE_HEIR
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('CUSTOMER') AND O.NAME = 'FK_CUSTOMER_COULD_BE__USER')
ALTER TABLE CUSTOMER
   DROP CONSTRAINT FK_CUSTOMER_COULD_BE__USER
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('CUSTOMER_PLAN') AND O.NAME = 'FK_CUSTOMER_AVOIR_UN__PLAN')
ALTER TABLE CUSTOMER_PLAN
   DROP CONSTRAINT FK_CUSTOMER_AVOIR_UN__PLAN
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('CUSTOMER_PLAN') AND O.NAME = 'FK_CUSTOMER_CUST_HAVE_CUSTOMER')
ALTER TABLE CUSTOMER_PLAN
   DROP CONSTRAINT FK_CUSTOMER_CUST_HAVE_CUSTOMER
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('HEIR') AND O.NAME = 'FK_HEIR_EST__USER')
ALTER TABLE HEIR
   DROP CONSTRAINT FK_HEIR_EST__USER
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('HISTORIQUE_USER') AND O.NAME = 'FK_HISTORIQ_AURA__USER')
ALTER TABLE HISTORIQUE_USER
   DROP CONSTRAINT FK_HISTORIQ_AURA__USER
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('HISTORIQUE_USER') AND O.NAME = 'FK_HISTORIQ_EST_HISTO_TYPE_HIS')
ALTER TABLE HISTORIQUE_USER
   DROP CONSTRAINT FK_HISTORIQ_EST_HISTO_TYPE_HIS
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('PAYMENT_HISTORY') AND O.NAME = 'FK_PAYMENT__PAID_CUSTOMER')
ALTER TABLE PAYMENT_HISTORY
   DROP CONSTRAINT FK_PAYMENT__PAID_CUSTOMER
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('STRONGBOXCONTENTWALLETCEX') AND O.NAME = 'FK_STRONGBO_AVOIRTYPE_TYPE_WAL5')
ALTER TABLE STRONGBOXCONTENTWALLETCEX
   DROP CONSTRAINT FK_STRONGBO_AVOIRTYPE_TYPE_WAL5
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('STRONGBOXCONTENTWALLETDESKTOP') AND O.NAME = 'FK_STRONGBO_AVOIRTYPE_TYPE_WAL3')
ALTER TABLE STRONGBOXCONTENTWALLETDESKTOP
   DROP CONSTRAINT FK_STRONGBO_AVOIRTYPE_TYPE_WAL3
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('STRONGBOXCONTENTWALLETDEX') AND O.NAME = 'FK_STRONGBO_AVOIRTYPE_TYPE_WAL')
ALTER TABLE STRONGBOXCONTENTWALLETDEX
   DROP CONSTRAINT FK_STRONGBO_AVOIRTYPE_TYPE_WAL
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('STRONGBOXCONTENTWALLETHARDWARE') AND O.NAME = 'FK_STRONGBO_AVOIRTYPE_TYPE_WAL2')
ALTER TABLE STRONGBOXCONTENTWALLETHARDWARE
   DROP CONSTRAINT FK_STRONGBO_AVOIRTYPE_TYPE_WAL2
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('STRONGBOXCONTENTWALLETMOBILE') AND O.NAME = 'FK_STRONGBO_AVOIRTYPE_TYPE_WAL4')
ALTER TABLE STRONGBOXCONTENTWALLETMOBILE
   DROP CONSTRAINT FK_STRONGBO_AVOIRTYPE_TYPE_WAL4
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('STRONGBOXFORHEIR') AND O.NAME = 'FK_STRONGBO_AVOIR_ACT_POLICY')
ALTER TABLE STRONGBOXFORHEIR
   DROP CONSTRAINT FK_STRONGBO_AVOIR_ACT_POLICY
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('STRONGBOXFORHEIR') AND O.NAME = 'FK_STRONGBO_AVOIR_CUS_CUSTOMER')
ALTER TABLE STRONGBOXFORHEIR
   DROP CONSTRAINT FK_STRONGBO_AVOIR_CUS_CUSTOMER
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('STRONGBOXPERSONNAL') AND O.NAME = 'FK_STRONGBO_AVOIR_POL_POLICY')
ALTER TABLE STRONGBOXPERSONNAL
   DROP CONSTRAINT FK_STRONGBO_AVOIR_POL_POLICY
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('STRONGBOXPERSONNAL') AND O.NAME = 'FK_STRONGBO_PEUT_AVOI_CUSTOMER')
ALTER TABLE STRONGBOXPERSONNAL
   DROP CONSTRAINT FK_STRONGBO_PEUT_AVOI_CUSTOMER
go

IF EXISTS (SELECT 1
   FROM DBO.SYSREFERENCES R JOIN DBO.SYSOBJECTS O ON (O.ID = R.CONSTID AND O.TYPE = 'F')
   WHERE R.FKEYID = OBJECT_ID('WALLET_CONNECTION') AND O.NAME = 'FK_WALLET_C_AVOIR_WAL__USER')
ALTER TABLE WALLET_CONNECTION
   DROP CONSTRAINT FK_WALLET_C_AVOIR_WAL__USER
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('ACTIVITY_VERIFICATION')
            AND   NAME  = 'NEED_STRONGBOXFORHEIR_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX ACTIVITY_VERIFICATION.NEED_STRONGBOXFORHEIR_FK
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('ACTIVITY_VERIFICATION')
            AND   NAME  = 'NEED_USER_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX ACTIVITY_VERIFICATION.NEED_USER_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('ACTIVITY_VERIFICATION')
            AND   TYPE = 'U')
   DROP TABLE ACTIVITY_VERIFICATION
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('AFFILIATE')
            AND   NAME  = 'AFFILIATE2_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX AFFILIATE.AFFILIATE2_FK
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('AFFILIATE')
            AND   NAME  = 'AFFILIATE_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX AFFILIATE.AFFILIATE_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('AFFILIATE')
            AND   TYPE = 'U')
   DROP TABLE AFFILIATE
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('CUSTOMER')
            AND   TYPE = 'U')
   DROP TABLE CUSTOMER
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('CUSTOMER_PLAN')
            AND   NAME  = 'CUST_HAVE_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX CUSTOMER_PLAN.CUST_HAVE_FK
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('CUSTOMER_PLAN')
            AND   NAME  = 'AVOIR_UN_PLAN_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX CUSTOMER_PLAN.AVOIR_UN_PLAN_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('CUSTOMER_PLAN')
            AND   TYPE = 'U')
   DROP TABLE CUSTOMER_PLAN
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('HEIR')
            AND   TYPE = 'U')
   DROP TABLE HEIR
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('HISTORIQUE_USER')
            AND   NAME  = 'AURA_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX HISTORIQUE_USER.AURA_FK
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('HISTORIQUE_USER')
            AND   NAME  = 'EST_HISTO_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX HISTORIQUE_USER.EST_HISTO_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('HISTORIQUE_USER')
            AND   TYPE = 'U')
   DROP TABLE HISTORIQUE_USER
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('MAILP_PROVIDER')
            AND   TYPE = 'U')
   DROP TABLE MAILP_PROVIDER
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('PAYMENT_HISTORY')
            AND   NAME  = 'PAID_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX PAYMENT_HISTORY.PAID_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('PAYMENT_HISTORY')
            AND   TYPE = 'U')
   DROP TABLE PAYMENT_HISTORY
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('"PLAN"')
            AND   TYPE = 'U')
   DROP TABLE "PLAN"
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('POLICY')
            AND   TYPE = 'U')
   DROP TABLE POLICY
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('SOCIAL_NETWORK')
            AND   TYPE = 'U')
   DROP TABLE SOCIAL_NETWORK
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('STRONGBOXCONTENTWALLETCEX')
            AND   NAME  = 'ASSOCIATION_24_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX STRONGBOXCONTENTWALLETCEX.ASSOCIATION_24_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('STRONGBOXCONTENTWALLETCEX')
            AND   TYPE = 'U')
   DROP TABLE STRONGBOXCONTENTWALLETCEX
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('STRONGBOXCONTENTWALLETDESKTOP')
            AND   NAME  = 'ASSOCIATION_22_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX STRONGBOXCONTENTWALLETDESKTOP.ASSOCIATION_22_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('STRONGBOXCONTENTWALLETDESKTOP')
            AND   TYPE = 'U')
   DROP TABLE STRONGBOXCONTENTWALLETDESKTOP
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('STRONGBOXCONTENTWALLETDEX')
            AND   NAME  = 'ASSOCIATION_25_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX STRONGBOXCONTENTWALLETDEX.ASSOCIATION_25_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('STRONGBOXCONTENTWALLETDEX')
            AND   TYPE = 'U')
   DROP TABLE STRONGBOXCONTENTWALLETDEX
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('STRONGBOXCONTENTWALLETHARDWARE')
            AND   NAME  = 'ASSOCIATION_21_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX STRONGBOXCONTENTWALLETHARDWARE.ASSOCIATION_21_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('STRONGBOXCONTENTWALLETHARDWARE')
            AND   TYPE = 'U')
   DROP TABLE STRONGBOXCONTENTWALLETHARDWARE
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('STRONGBOXCONTENTWALLETMOBILE')
            AND   NAME  = 'ASSOCIATION_23_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX STRONGBOXCONTENTWALLETMOBILE.ASSOCIATION_23_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('STRONGBOXCONTENTWALLETMOBILE')
            AND   TYPE = 'U')
   DROP TABLE STRONGBOXCONTENTWALLETMOBILE
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('STRONGBOXFORHEIR')
            AND   NAME  = 'AVOIR_CUSTOMER_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX STRONGBOXFORHEIR.AVOIR_CUSTOMER_FK
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('STRONGBOXFORHEIR')
            AND   NAME  = 'AVOIR_ACTIVATION_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX STRONGBOXFORHEIR.AVOIR_ACTIVATION_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('STRONGBOXFORHEIR')
            AND   TYPE = 'U')
   DROP TABLE STRONGBOXFORHEIR
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('STRONGBOXPERSONNAL')
            AND   NAME  = 'AVOIR_POLICY_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX STRONGBOXPERSONNAL.AVOIR_POLICY_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('STRONGBOXPERSONNAL')
            AND   TYPE = 'U')
   DROP TABLE STRONGBOXPERSONNAL
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('TYPE_DATA')
            AND   TYPE = 'U')
   DROP TABLE TYPE_DATA
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('TYPE_HISTO')
            AND   TYPE = 'U')
   DROP TABLE TYPE_HISTO
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('TYPE_WALLET')
            AND   TYPE = 'U')
   DROP TABLE TYPE_WALLET
go

IF EXISTS (SELECT 1
            FROM  SYSINDEXES
           WHERE  ID    = OBJECT_ID('WALLET_CONNECTION')
            AND   NAME  = 'AVOIR_WALLET_FK'
            AND   INDID > 0
            AND   INDID < 255)
   DROP INDEX WALLET_CONNECTION.AVOIR_WALLET_FK
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('WALLET_CONNECTION')
            AND   TYPE = 'U')
   DROP TABLE WALLET_CONNECTION
go

IF EXISTS (SELECT 1
            FROM  SYSOBJECTS
           WHERE  ID = OBJECT_ID('_USER')
            AND   TYPE = 'U')
   DROP TABLE _USER
go

/*==============================================================*/
/* Table : ACTIVITY_VERIFICATION                                */
/*==============================================================*/
CREATE TABLE ACTIVITY_VERIFICATION (
   ID_ACTIVITY_VERIFICATION NUMERIC              IDENTITY,
   ID_STRONGBOXFORHEIR  NUMERIC              NOT NULL,
   ID_USER              NUMERIC              NOT NULL,
   DATE_EMAIL           VARCHAR(255)         NULL,
   DATE_CHECK           VARCHAR(255)         NULL,
   "CHECK"              BIT                  NULL,
   DECEADED             BIT                  NULL,
   IDPERIOD             VARCHAR(255)         NULL,
   ACTIVE               BIT                  NULL,
   DATE_DEATH           VARCHAR(255)         NULL,
   CONSTRAINT PK_ACTIVITY_VERIFICATION PRIMARY KEY NONCLUSTERED (ID_ACTIVITY_VERIFICATION)
)
go

/*==============================================================*/
/* Index : NEED_USER_FK                                         */
/*==============================================================*/
CREATE INDEX NEED_USER_FK ON ACTIVITY_VERIFICATION (
ID_USER ASC
)
go

/*==============================================================*/
/* Index : NEED_STRONGBOXFORHEIR_FK                             */
/*==============================================================*/
CREATE INDEX NEED_STRONGBOXFORHEIR_FK ON ACTIVITY_VERIFICATION (
ID_STRONGBOXFORHEIR ASC
)
go

/*==============================================================*/
/* Table : AFFILIATE                                            */
/*==============================================================*/
CREATE TABLE AFFILIATE (
   ID_CUSTOMER          NUMERIC              NOT NULL,
   ID_HEIR              NUMERIC              NOT NULL,
   AFFILIATE_DATE       VARCHAR(255)         NOT NULL,
   ID_USER_CUSTOMER     VARCHAR(255)         NOT NULL,
   ID_USER_HEIR         VARCHAR(255)         NOT NULL,
   HEIR_PUBLICKEY       VARCHAR(255)         NOT NULL,
   AFFILIATE_ACTIVE     BIT                  NULL,
   CONSTRAINT PK_AFFILIATE PRIMARY KEY (ID_CUSTOMER, ID_HEIR)
)
go

/*==============================================================*/
/* Index : AFFILIATE_FK                                         */
/*==============================================================*/
CREATE INDEX AFFILIATE_FK ON AFFILIATE (
ID_CUSTOMER ASC
)
go

/*==============================================================*/
/* Index : AFFILIATE2_FK                                        */
/*==============================================================*/
CREATE INDEX AFFILIATE2_FK ON AFFILIATE (
ID_HEIR ASC
)
go

/*==============================================================*/
/* Table : CUSTOMER                                             */
/*==============================================================*/
CREATE TABLE CUSTOMER (
   ID_CUSTOMER          NUMERIC              IDENTITY,
   ID_USER              NUMERIC              NOT NULL,
   CUSTOMER_NAME        VARCHAR(255)         NOT NULL,
   CUSTOMER_FIRSTNAME   VARCHAR(255)         NOT NULL,
   CUSTOMER_EMAIL       VARCHAR(255)         NOT NULL,
   CUSTOMER_PHONENUMBER VARCHAR(255)         NOT NULL,
   CUSTOMER_ACTIVE      BIT                  NULL,
   CUSTOMER_CREATION_DATE VARCHAR(255)         NULL,
   CONSTRAINT PK_CUSTOMER PRIMARY KEY NONCLUSTERED (ID_CUSTOMER)
)
go

/*==============================================================*/
/* Table : CUSTOMER_PLAN                                        */
/*==============================================================*/
CREATE TABLE CUSTOMER_PLAN (
   ID_CUST_PLAN         NUMERIC              IDENTITY,
   ID_CUSTOMER          NUMERIC              NOT NULL,
   ID_PLAN              NUMERIC              NOT NULL,
   STARTDATE            VARCHAR(255)         NOT NULL,
   ENDDATE              VARCHAR(255)         NULL,
   CONSTRAINT PK_CUSTOMER_PLAN PRIMARY KEY NONCLUSTERED (ID_CUST_PLAN)
)
go

/*==============================================================*/
/* Index : AVOIR_UN_PLAN_FK                                     */
/*==============================================================*/
CREATE INDEX AVOIR_UN_PLAN_FK ON CUSTOMER_PLAN (
ID_PLAN ASC
)
go

/*==============================================================*/
/* Index : CUST_HAVE_FK                                         */
/*==============================================================*/
CREATE INDEX CUST_HAVE_FK ON CUSTOMER_PLAN (
ID_CUSTOMER ASC
)
go

/*==============================================================*/
/* Table : HEIR                                                 */
/*==============================================================*/
CREATE TABLE HEIR (
   ID_HEIR              NUMERIC              IDENTITY,
   ID_USER              NUMERIC              NOT NULL,
   HEIR_LASTNAME        VARCHAR(255)         NOT NULL,
   HEIR_FIRSTNAME       VARCHAR(255)         NOT NULL,
   HEIR_PHONENUMBER     VARCHAR(255)         NOT NULL,
   HEIR_EMAIL           VARCHAR(255)         NOT NULL,
   HEIR_ACTIVE          BIT                  NULL,
   HEIR_CREATION_DATE   VARCHAR(255)         NULL,
   HEIR_WALLETTYPE      VARCHAR(3060)        NULL,
   HEIR_PUBLIC_KEY      VARCHAR(255)         NULL,
   HEIR_ID_TYPE_WALLET  INT                  NULL,
   CONSTRAINT PK_HEIR PRIMARY KEY NONCLUSTERED (ID_HEIR)
)
go

/*==============================================================*/
/* Table : HISTORIQUE_USER                                      */
/*==============================================================*/
CREATE TABLE HISTORIQUE_USER (
   ID_HISTORIQUE        NUMERIC              IDENTITY,
   ID_USER              NUMERIC              NOT NULL,
   ID_TYPE_HISTO        NUMERIC              NOT NULL,
   COMMENT              VARCHAR(512)         NOT NULL,
   HISTO_DATE           VARCHAR(255)         NOT NULL,
   HISTO_ACTIVE         BIT                  NULL,
   CONSTRAINT PK_HISTORIQUE_USER PRIMARY KEY NONCLUSTERED (ID_HISTORIQUE)
)
go

/*==============================================================*/
/* Index : EST_HISTO_FK                                         */
/*==============================================================*/
CREATE INDEX EST_HISTO_FK ON HISTORIQUE_USER (
ID_TYPE_HISTO ASC
)
go

/*==============================================================*/
/* Index : AURA_FK                                              */
/*==============================================================*/
CREATE INDEX AURA_FK ON HISTORIQUE_USER (
ID_USER ASC
)
go

/*==============================================================*/
/* Table : MAILP_PROVIDER                                       */
/*==============================================================*/
CREATE TABLE MAILP_PROVIDER (
   ID_MAIL_PROVIDER     NUMERIC              IDENTITY,
   PROVIDE_LABEL        VARCHAR(255)         NULL,
   CONSTRAINT PK_MAILP_PROVIDER PRIMARY KEY NONCLUSTERED (ID_MAIL_PROVIDER)
)
go

/*==============================================================*/
/* Table : PAYMENT_HISTORY                                      */
/*==============================================================*/
CREATE TABLE PAYMENT_HISTORY (
   ID_PAYMENT           NUMERIC              IDENTITY,
   ID_CUST_PLAN         NUMERIC              NOT NULL,
   CARDNUMBER           VARCHAR(255)         NULL,
   PAYMENTDATE          VARCHAR(255)         NULL,
   ISPAID               BIT                  NULL,
   AMONT_MONTH          VARCHAR(255)         NULL,
   AMOUNT_YEAR          VARCHAR(255)         NULL,
   CURRENCY             VARCHAR(255)         NULL,
   WALLETPUBLICKEY      VARCHAR(2096)        NULL,
   PAIEMENTTYPE         VARCHAR(255)         NULL,
   AMONT_YEAR_WITH_TAX  CHAR(10)             NULL,
   VAT                  VARCHAR(255)         NULL,
   INVOICE_NUMBER       INT                  NULL,
   CUSTOMER_FIRSTNAME   VARCHAR(255)         NULL,
   CUSTOMER_LASTNAME    VARCHAR(255)         NULL,
   CUSTOMER_EMAIL       VARCHAR(255)         NULL,
   PROVIDER_NAME        VARCHAR(255)         NULL,
   PROVIDER_ADRESSLINE1 VARCHAR(255)         NULL,
   PROVIDER_ADRESSLINE2 VARCHAR(255)         NULL,
   PROVIDER_EMAIL       VARCHAR(255)         NULL,
   PROVIDER_REGISTRATIONNUMBER VARCHAR(255)         NULL,
   PROVIDER_REGISTRATIONPLACE VARCHAR(255)         NULL,
   PROVIDER_VATNUMBER   VARCHAR(255)         NULL,
   CONSTRAINT PK_PAYMENT_HISTORY PRIMARY KEY NONCLUSTERED (ID_PAYMENT)
)
go

/*==============================================================*/
/* Index : PAID_FK                                              */
/*==============================================================*/
CREATE INDEX PAID_FK ON PAYMENT_HISTORY (
ID_CUST_PLAN ASC
)
go

/*==============================================================*/
/* Table : "PLAN"                                               */
/*==============================================================*/
CREATE TABLE "PLAN" (
   ID_PLAN              NUMERIC              IDENTITY,
   PRICE                VARCHAR(255)         NULL,
   LABEL                VARCHAR(255)         NULL,
   NB_HEIRPERSTRONGBOX  VARCHAR(255)         NULL,
   NB_PERSONALSTRONGBOXES VARCHAR(255)         NULL,
   NB_HEIRSTRONGBOXES   VARCHAR(255)         NULL,
   CONSTRAINT PK_PLAN PRIMARY KEY NONCLUSTERED (ID_PLAN)
)
go

/*==============================================================*/
/* Table : POLICY                                               */
/*==============================================================*/
CREATE TABLE POLICY (
   ID_POLICY            NUMERIC              IDENTITY,
   POLICY_VALUE         VARCHAR(255)         NOT NULL,
   CONSTRAINT PK_POLICY PRIMARY KEY NONCLUSTERED (ID_POLICY)
)
go

/*==============================================================*/
/* Table : SOCIAL_NETWORK                                       */
/*==============================================================*/
CREATE TABLE SOCIAL_NETWORK (
   ID_SOCIALNETWORK     NUMERIC              IDENTITY,
   SN_LABEL             VARCHAR(255)         NULL,
   CONSTRAINT PK_SOCIAL_NETWORK PRIMARY KEY NONCLUSTERED (ID_SOCIALNETWORK)
)
go

/*==============================================================*/
/* Table : STRONGBOXCONTENTWALLETCEX                            */
/*==============================================================*/
CREATE TABLE STRONGBOXCONTENTWALLETCEX (
   ID_STRONGBOXCONTENTWALLETCEX NUMERIC              IDENTITY,
   IDTYPEWALLET         NUMERIC              NOT NULL,
   ID_STRONGBOXFORHEIR  NUMERIC               NULL,
   ID_STRONGBOXPERSONNAL NUMERIC             NULL,
   LABEL                VARCHAR(255)         NULL,
   DATEADDED            VARCHAR(255)         NULL,
   DATEUPDATED          VARCHAR(255)         NULL,
   LOGIN                VARCHAR(255)         NULL,
   PASSWORD             VARCHAR(255)         NULL,
   ACTIVE               BIT                  NULL,
   ASSIGNED             BIT                  NULL,
   PROVIDER             VARCHAR(255)         NULL,
   CONSTRAINT PK_STRONGBOXCONTENTWALLETCEX PRIMARY KEY NONCLUSTERED (ID_STRONGBOXCONTENTWALLETCEX)
)
go

/*==============================================================*/
/* Index : ASSOCIATION_24_FK                                    */
/*==============================================================*/
CREATE INDEX ASSOCIATION_24_FK ON STRONGBOXCONTENTWALLETCEX (
IDTYPEWALLET ASC
)
go

/*==============================================================*/
/* Table : STRONGBOXCONTENTWALLETDESKTOP                        */
/*==============================================================*/
CREATE TABLE STRONGBOXCONTENTWALLETDESKTOP (
   ID_STRONGBOXCONTENTWALLETDESKTOP NUMERIC              IDENTITY,
   IDTYPEWALLET         NUMERIC              NOT NULL,
   ID_STRONGBOXFORHEIR  NUMERIC               NULL,
   ID_STRONGBOXPERSONNAL NUMERIC             NULL,
   LABEL                VARCHAR(255)         NULL,
   DATEADDED            VARCHAR(255)         NULL,
   DATEUPDATED          VARCHAR(255)         NULL,
   LOGIN                VARCHAR(255)         NULL,
   PASSWORD             VARCHAR(255)         NULL,
   ACTIVE               BIT                  NULL,
   ASSIGNED             BIT                  NULL,
   PROVIDER             VARCHAR(255)         NULL,
   CONSTRAINT PK_STRONGBOXCONTENTWALLETDESKT PRIMARY KEY NONCLUSTERED (ID_STRONGBOXCONTENTWALLETDESKTOP)
)
go

/*==============================================================*/
/* Index : ASSOCIATION_22_FK                                    */
/*==============================================================*/
CREATE INDEX ASSOCIATION_22_FK ON STRONGBOXCONTENTWALLETDESKTOP (
IDTYPEWALLET ASC
)
go

/*==============================================================*/
/* Table : STRONGBOXCONTENTWALLETDEX                            */
/*==============================================================*/
CREATE TABLE STRONGBOXCONTENTWALLETDEX (
   ID_STRONGBOXCONTENTWALLETDEX_ NUMERIC              IDENTITY,
   IDTYPEWALLET         NUMERIC              NOT NULL,
   ID_STRONGBOXFORHEIR  NUMERIC               NULL,
   ID_STRONGBOXPERSONNAL NUMERIC             NULL,
   LABEL                VARCHAR(255)         NULL,
   DATEADDED            VARCHAR(255)         NULL,
   DATEUPDATED          VARCHAR(255)         NULL,
   SEED                 VARCHAR(512)         NULL,
   ACTIVE               BIT                  NULL,
   ASSIGNED             BIT                  NULL,
   PROVIDER             VARCHAR(255)         NULL,
   CONSTRAINT PK_STRONGBOXCONTENTWALLETDEX PRIMARY KEY NONCLUSTERED (ID_STRONGBOXCONTENTWALLETDEX_)
)
go

/*==============================================================*/
/* Index : ASSOCIATION_25_FK                                    */
/*==============================================================*/
CREATE INDEX ASSOCIATION_25_FK ON STRONGBOXCONTENTWALLETDEX (
IDTYPEWALLET ASC
)
go

/*==============================================================*/
/* Table : STRONGBOXCONTENTWALLETHARDWARE                       */
/*==============================================================*/
CREATE TABLE STRONGBOXCONTENTWALLETHARDWARE (
   ID_STRONGBOXCONTENTWALLETHARDWARE NUMERIC              IDENTITY,
   IDTYPEWALLET         NUMERIC              NOT NULL,
   ID_STRONGBOXFORHEIR  NUMERIC               NULL,
   ID_STRONGBOXPERSONNAL NUMERIC             NULL,
   LABEL                VARCHAR(255)         NULL,
   DATEADDED            VARCHAR(255)         NULL,
   DATEUPDATED          VARCHAR(255)         NULL,
   PASSWORD             VARCHAR(255)         NULL,
   ACTIVE               BIT                  NULL,
   ASSIGNED             BIT                  NULL,
   CONSTRAINT PK_STRONGBOXCONTENTWALLETHARDW PRIMARY KEY NONCLUSTERED (ID_STRONGBOXCONTENTWALLETHARDWARE)
)
go

/*==============================================================*/
/* Index : ASSOCIATION_21_FK                                    */
/*==============================================================*/
CREATE INDEX ASSOCIATION_21_FK ON STRONGBOXCONTENTWALLETHARDWARE (
IDTYPEWALLET ASC
)
go

/*==============================================================*/
/* Table : STRONGBOXCONTENTWALLETMOBILE                         */
/*==============================================================*/
CREATE TABLE STRONGBOXCONTENTWALLETMOBILE (
   ID_STRONGBOXCONTENTWALLETMOBILE_ NUMERIC              IDENTITY,
   IDTYPEWALLET         NUMERIC              NOT NULL,
   ID_STRONGBOXFORHEIR  NUMERIC               NULL,
   ID_STRONGBOXPERSONNAL NUMERIC             NULL,
   LABEL                VARCHAR(255)         NULL,
   DATEADDED            VARCHAR(255)         NULL,
   DATEUPDATED          VARCHAR(255)         NULL,
   LOGIN                VARCHAR(255)         NULL,
   PASSWORD             VARCHAR(255)         NULL,
   ACTIVE               BIT                  NULL,
   ASSIGNED             BIT                  NULL,
   PROVIDER             VARCHAR(255)         NULL,
   CONSTRAINT PK_STRONGBOXCONTENTWALLETMOBIL PRIMARY KEY NONCLUSTERED (ID_STRONGBOXCONTENTWALLETMOBILE_)
)
go

/*==============================================================*/
/* Index : ASSOCIATION_23_FK                                    */
/*==============================================================*/
CREATE INDEX ASSOCIATION_23_FK ON STRONGBOXCONTENTWALLETMOBILE (
IDTYPEWALLET ASC
)
go

/*==============================================================*/
/* Table : STRONGBOXFORHEIR                                     */
/*==============================================================*/
CREATE TABLE STRONGBOXFORHEIR (
   ID_STRONGBOXFORHEIR  NUMERIC              IDENTITY,
   ID_POLICY            NUMERIC              NOT NULL,
   ID_CUSTOMER          NUMERIC              NOT NULL,
   LABEL                VARCHAR(255)         NULL,
   MESSAGE_FOR_HEIRS    VARCHAR(6000)       NULL,
   DISPLAY              BIT                  NULL,
   LIST_OF_HEIRS        VARCHAR(512)         NULL,
   ACTIVE               BIT                  NULL,
   WALLETPUBLICKEY      VARCHAR(2096)        NULL,
   USER_SECRETPK        VARCHAR(3060)        NULL,
   USER_SOLANAPK        VARCHAR(2096)        NULL,
   NFT_SHARD_SERENITY   VARCHAR(2096)        NULL,
   NFT_SHARD_USER       VARCHAR(2096)        NULL,
   NFT_SHARD_HEIR       VARCHAR(2096)        NULL,
   SCPK                 VARCHAR(2096)        NULL,
   CODEID               VARCHAR(2096)        NULL,
   PREPAYEDINHERENCE    VARCHAR(2096)        NULL,
   DATEADDED            VARCHAR(255)         NULL,
   UNLOCKED             BIT                  NULL,
   PAID                 BIT                  NULL,
   CONSTRAINT PK_STRONGBOXFORHEIR PRIMARY KEY NONCLUSTERED (ID_STRONGBOXFORHEIR)
)
go

/*==============================================================*/
/* Index : AVOIR_ACTIVATION_FK                                  */
/*==============================================================*/
CREATE INDEX AVOIR_ACTIVATION_FK ON STRONGBOXFORHEIR (
ID_POLICY ASC
)
go

/*==============================================================*/
/* Index : AVOIR_CUSTOMER_FK                                    */
/*==============================================================*/
CREATE INDEX AVOIR_CUSTOMER_FK ON STRONGBOXFORHEIR (
ID_CUSTOMER ASC
)
go

/*==============================================================*/
/* Table : STRONGBOXPERSONNAL                                   */
/*==============================================================*/
CREATE TABLE STRONGBOXPERSONNAL (
   ID_STRONGBOXPERSONNAL NUMERIC              IDENTITY,
   ID_CUSTOMER          NUMERIC              NOT NULL,
   ID_POLICY            NUMERIC              NOT NULL,
   LABEL                VARCHAR(255)         NULL,
   ACTIVE               BIT                  NULL,
   WALLETPUBLICKEY      VARCHAR(2096)        NULL,
   USER_SECRETPK        VARCHAR(3060)        NULL,
   USER_PAYINGPK        VARCHAR(2096)        NULL,
   USER_SOLANAPK        VARCHAR(2096)        NULL,
   NFT_SHARD_SERENITY   VARCHAR(2096)        NULL,
   NFT_SHARD_USER       VARCHAR(2096)        NULL,
   NFT_SHARD_HEIR       VARCHAR(2096)        NULL,
   SCPK                 VARCHAR(2096)        NULL,
   CODEID               VARCHAR(2096)        NULL,
   DATEADDED            VARCHAR(255)         NULL,
   CONSTRAINT PK_STRONGBOXPERSONNAL PRIMARY KEY NONCLUSTERED (ID_STRONGBOXPERSONNAL)
)
go

/*==============================================================*/
/* Index : AVOIR_POLICY_FK                                      */
/*==============================================================*/
CREATE INDEX AVOIR_POLICY_FK ON STRONGBOXPERSONNAL (
ID_POLICY ASC
)
go

/*==============================================================*/
/* Table : TYPE_DATA                                            */
/*==============================================================*/
CREATE TABLE TYPE_DATA (
   IDTYPEDATA           NUMERIC              IDENTITY,
   DATA_LABEL           VARCHAR(255)         NULL,
   CONSTRAINT PK_TYPE_DATA PRIMARY KEY NONCLUSTERED (IDTYPEDATA)
)
go

/*==============================================================*/
/* Table : TYPE_HISTO                                           */
/*==============================================================*/
CREATE TABLE TYPE_HISTO (
   ID_TYPE_HISTO        NUMERIC              IDENTITY,
   TYPE_LABEL           VARCHAR(255)         NULL,
   CONSTRAINT PK_TYPE_HISTO PRIMARY KEY NONCLUSTERED (ID_TYPE_HISTO)
)
go

/*==============================================================*/
/* Table : TYPE_WALLET                                          */
/*==============================================================*/
CREATE TABLE TYPE_WALLET (
   IDTYPEWALLET         NUMERIC              IDENTITY,
   LABEL                VARCHAR(255)         NULL,
   TYPE                 VARCHAR(255)         NULL,
   SUPPORTED            BIT                  NULL,
   CONSTRAINT PK_TYPE_WALLET PRIMARY KEY NONCLUSTERED (IDTYPEWALLET)
)
go

/*==============================================================*/
/* Table : WALLET_CONNECTION                                    */
/*==============================================================*/
CREATE TABLE WALLET_CONNECTION (
   ID_WALLET            NUMERIC              IDENTITY,
   ID_USER              NUMERIC              NOT NULL,
   WALLET_LABEL         VARCHAR(255)         NULL,
   WALLET_PUBLICKEY     VARCHAR(3060)        NOT NULL,
   WALET_TYPE           VARCHAR(255)         NULL,
   WALLET_ISHEIR        BIT                  NULL,
   WALLET_ISCUSTOMER    BIT                  NULL,
   WALLET__ACTIVE       BIT                  NULL,
   ID_USERCUSTOMER      VARCHAR(255)         NULL,
   ID_TYPE_WALLET       INT                  NULL,
   USER_SECRETPK        VARCHAR(3060)        NULL,
   METAMASKPK           VARCHAR(3060)        NULL,
   CHAINID              VARCHAR(3060)        NULL,
   CONSTRAINT PK_WALLET_CONNECTION PRIMARY KEY NONCLUSTERED (ID_WALLET)
)
go

/*==============================================================*/
/* Index : AVOIR_WALLET_FK                                      */
/*==============================================================*/
CREATE INDEX AVOIR_WALLET_FK ON WALLET_CONNECTION (
ID_USER ASC
)
go

/*==============================================================*/
/* Table : _USER                                                */
/*==============================================================*/
CREATE TABLE _USER (
   ID_USER              NUMERIC              IDENTITY,
   LASTNAME             VARCHAR(255)         NOT NULL,
   FIRSTNAME            VARCHAR(255)         NOT NULL,
   EMAIL                BIT                  NOT NULL,
   PASSPORT             VARCHAR(255)         NULL,
   ADDSECURITY          VARCHAR(255)         NULL,
   IDCARDNUMBER         VARCHAR(255)         NULL,
   PHONENUMBER          VARCHAR(255)         NOT NULL,
   IS_CUSTOMER          BIT                  NULL,
   IS_HEIR              BIT                  NULL,
   EMAILVERIFICATIONCODE VARCHAR(255)         NULL,
   SMSVERIFICATIONCODE  VARCHAR(255)         NULL,
   VERIFIED             BIT                  NULL,
   ACTIVE               BIT                  NULL,
   CONSTRAINT PK__USER PRIMARY KEY NONCLUSTERED (ID_USER)
)
go

ALTER TABLE ACTIVITY_VERIFICATION
   ADD CONSTRAINT FK_ACTIVITY_NEED_STRO_STRONGBO FOREIGN KEY (ID_STRONGBOXFORHEIR)
      REFERENCES STRONGBOXFORHEIR (ID_STRONGBOXFORHEIR)
go

ALTER TABLE ACTIVITY_VERIFICATION
   ADD CONSTRAINT FK_ACTIVITY_NEED_USER__USER FOREIGN KEY (ID_USER)
      REFERENCES _USER (ID_USER)
go

ALTER TABLE AFFILIATE
   ADD CONSTRAINT FK_AFFILIAT_AFFILIATE_CUSTOMER FOREIGN KEY (ID_CUSTOMER)
      REFERENCES CUSTOMER (ID_CUSTOMER)
go

ALTER TABLE AFFILIATE
   ADD CONSTRAINT FK_AFFILIAT_AFFILIATE_HEIR FOREIGN KEY (ID_HEIR)
      REFERENCES HEIR (ID_HEIR)
go

ALTER TABLE CUSTOMER
   ADD CONSTRAINT FK_CUSTOMER_COULD_BE__USER FOREIGN KEY (ID_USER)
      REFERENCES _USER (ID_USER)
go

ALTER TABLE CUSTOMER_PLAN
   ADD CONSTRAINT FK_CUSTOMER_AVOIR_UN__PLAN FOREIGN KEY (ID_PLAN)
      REFERENCES "PLAN" (ID_PLAN)
go

ALTER TABLE CUSTOMER_PLAN
   ADD CONSTRAINT FK_CUSTOMER_CUST_HAVE_CUSTOMER FOREIGN KEY (ID_CUSTOMER)
      REFERENCES CUSTOMER (ID_CUSTOMER)
go

ALTER TABLE HEIR
   ADD CONSTRAINT FK_HEIR_EST__USER FOREIGN KEY (ID_USER)
      REFERENCES _USER (ID_USER)
go

ALTER TABLE HISTORIQUE_USER
   ADD CONSTRAINT FK_HISTORIQ_AURA__USER FOREIGN KEY (ID_USER)
      REFERENCES _USER (ID_USER)
go

ALTER TABLE HISTORIQUE_USER
   ADD CONSTRAINT FK_HISTORIQ_EST_HISTO_TYPE_HIS FOREIGN KEY (ID_TYPE_HISTO)
      REFERENCES TYPE_HISTO (ID_TYPE_HISTO)
go

ALTER TABLE PAYMENT_HISTORY
   ADD CONSTRAINT FK_PAYMENT__PAID_CUSTOMER FOREIGN KEY (ID_CUST_PLAN)
      REFERENCES CUSTOMER_PLAN (ID_CUST_PLAN)
go

ALTER TABLE STRONGBOXCONTENTWALLETCEX
   ADD CONSTRAINT FK_STRONGBO_AVOIRTYPE_TYPE_WAL5 FOREIGN KEY (IDTYPEWALLET)
      REFERENCES TYPE_WALLET (IDTYPEWALLET)
go

ALTER TABLE STRONGBOXCONTENTWALLETDESKTOP
   ADD CONSTRAINT FK_STRONGBO_AVOIRTYPE_TYPE_WAL3 FOREIGN KEY (IDTYPEWALLET)
      REFERENCES TYPE_WALLET (IDTYPEWALLET)
go

ALTER TABLE STRONGBOXCONTENTWALLETDEX
   ADD CONSTRAINT FK_STRONGBO_AVOIRTYPE_TYPE_WAL FOREIGN KEY (IDTYPEWALLET)
      REFERENCES TYPE_WALLET (IDTYPEWALLET)
go

ALTER TABLE STRONGBOXCONTENTWALLETHARDWARE
   ADD CONSTRAINT FK_STRONGBO_AVOIRTYPE_TYPE_WAL2 FOREIGN KEY (IDTYPEWALLET)
      REFERENCES TYPE_WALLET (IDTYPEWALLET)
go

ALTER TABLE STRONGBOXCONTENTWALLETMOBILE
   ADD CONSTRAINT FK_STRONGBO_AVOIRTYPE_TYPE_WAL4 FOREIGN KEY (IDTYPEWALLET)
      REFERENCES TYPE_WALLET (IDTYPEWALLET)
go

ALTER TABLE STRONGBOXFORHEIR
   ADD CONSTRAINT FK_STRONGBO_AVOIR_ACT_POLICY FOREIGN KEY (ID_POLICY)
      REFERENCES POLICY (ID_POLICY)
go

ALTER TABLE STRONGBOXFORHEIR
   ADD CONSTRAINT FK_STRONGBO_AVOIR_CUS_CUSTOMER FOREIGN KEY (ID_CUSTOMER)
      REFERENCES CUSTOMER (ID_CUSTOMER)
go

ALTER TABLE STRONGBOXPERSONNAL
   ADD CONSTRAINT FK_STRONGBO_AVOIR_POL_POLICY FOREIGN KEY (ID_POLICY)
      REFERENCES POLICY (ID_POLICY)
go

ALTER TABLE STRONGBOXPERSONNAL
   ADD CONSTRAINT FK_STRONGBO_PEUT_AVOI_CUSTOMER FOREIGN KEY (ID_CUSTOMER)
      REFERENCES CUSTOMER (ID_CUSTOMER)
go

ALTER TABLE WALLET_CONNECTION
   ADD CONSTRAINT FK_WALLET_C_AVOIR_WAL__USER FOREIGN KEY (ID_USER)
      REFERENCES _USER (ID_USER)
go

