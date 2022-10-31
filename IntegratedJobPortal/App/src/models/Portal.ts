export class Portal {
    PortalId: number;
    PortalName: string;
    CreateBy: string;
    CreatedDate: Date;

    constructor(
        PortalId: number,
        PortalName: string,
        CreateBy: string,
        CreatedDate: Date
    ) {
        this.PortalId = PortalId;
        this.PortalName = PortalName;
        this.CreateBy = CreateBy;
        this.CreatedDate = CreatedDate;
    }
}

export class PortalEntity {
    PortalId: number;
    PortalName: string;
    CreateBy: number;
    UAID: number;
    LoginURL: string;
    LogOutURL: string;
    RedirectURL: string;

    constructor(
        PortalId: number,
        PortalName: string,
        CreateBy: number,
        UAID: number,
        LoginURL: string,
        LogOutURL: string,
        RedirectURL: string,
    ) {
        this.PortalId = PortalId;
        this.PortalName = PortalName;
        this.CreateBy = CreateBy;
        this.UAID = UAID;
        this.LoginURL = LoginURL;
        this.LogOutURL = LogOutURL;
        this.RedirectURL = RedirectURL;
    }
}

export interface JobPortal {
    portalId: number;
    portalName: number;
    portalAccountId: string;
    portalLoginId: string;
    portalPassword: string;
    isFreeze: boolean;
    createDate: string;
    modifiedDate: string;
    createdBy: number;
    modifiedBy: number;
    accountStartDate: string;
    accountExpiryDate: string;
    contactPerson: string;
    remarks: string;
    portalUsesTerms: string;
    defaultSiteTime: number;
    isInActiveForADay: boolean;
    portalInactiveTillDate: string;
    resumeViewsLimit: number;
}

export interface JobPortalEntity {
    portalId: number;
    portalAccountId: string;
    portalLoginId: string;
    portalPassword: string;
    isFreeze: boolean;
    createdBy: number;
    modifiedBy: number;
    accountStartDate: string;
    accountExpiryDate: string;
    contactPerson: string;
    remarks: string;
    portalUsesTerms: string;
    defaultSiteTime: number;
    isInActiveForADay: boolean;
    portalInactiveTillDate: string;
    resumeViewsLimit: number;
}

