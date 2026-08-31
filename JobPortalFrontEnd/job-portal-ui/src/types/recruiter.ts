export interface RecruiterProfile {
    id: number;
    userId: number;
    companyId: number | null;

    firstName: string;
    lastName: string;

    designation: string | null;
    department: string | null;
    phone: string | null;
    linkedinUrl: string | null;
}

export interface RecruiterProfileRequest {
    companyId: number | null;

    firstName: string;
    lastName: string;

    designation: string | null;
    department: string | null;
    phone: string | null;
    linkedinUrl: string | null;
}