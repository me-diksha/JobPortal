export interface RegisterRequest {
    FirstName: string;
    LastName: string;
    Country?: string;

    CompanyId?: number | null;
    CompanyName?: string | null;

    Email: string;
    Password: string;
    RoleId: number;
}