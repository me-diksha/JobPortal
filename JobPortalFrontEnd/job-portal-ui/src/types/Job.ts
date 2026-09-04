export interface Job {
    id: number;
    title: string;
    description: string;
    location?: string;
    employmentType?: string;
    experienceLevel?: string;
    minSalary?: number;
    maxSalary?: number;
    status?: string;
    deadline?: string;
    companyName?: string;
    createdAt?: string;
}

export interface JobRequest {
    title: string;
    description: string;
    location?: string;
    refEmploymentType?: number;
    experienceLevel?: string;
    minSalary?: number;
    maxSalary?: number;
    deadline?: string;
}