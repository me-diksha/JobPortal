export interface Job {
    id: number;
    title: string;
    description: string;
    location?: string;
    refEmploymentType: number;
    employmentType?: string;
    experienceLevel?: string;
    minSalary?: number;
    maxSalary?: number;
    deadline?: string;
    status: string;
    companyName: string;
}