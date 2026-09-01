export interface Company {
    id: number;
    name: string;
}
export interface CompanyDetails {
    id: number;
    name: string;
    description?: string | null;
    industry?: string | null;
    website?: string | null;
    email?: string | null;
    phone?: string | null;
    addressLine1?: string | null;
    addressLine2?: string | null;
    city?: string | null;
    state?: string | null;
    country?: string | null;
    postalCode?: string | null;
    logosUrl?: string | null;
    companySize?: number | null;
    foundedYear?: number | null;
}

export interface CompanyDetailsRequest {
    name: string;
    description?: string | null;
    industry?: string | null;
    website?: string | null;
    email?: string | null;
    phone?: string | null;
    addressLine1?: string | null;
    addressLine2?: string | null;
    city?: string | null;
    state?: string | null;
    country?: string | null;
    postalCode?: string | null;
    logosUrl?: string | null;
    companySize?: number | null;
    foundedYear?: number | null;
}