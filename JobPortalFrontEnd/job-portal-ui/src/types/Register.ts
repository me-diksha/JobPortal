export interface RegisterRequest {
    FirstName: string;
    LastName: string;
    Country?: string | null;

    CompanyId?: number | null;
    CompanyName?: string | null;

    Email: string;
    Password: string;
    RoleId: number;
}