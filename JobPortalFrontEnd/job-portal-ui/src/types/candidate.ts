export interface CandidateProfile {
    id: number;

    headline: string | null;
    bio: string | null;

    addressLine1: string | null;
    addressLine2: string | null;

    city: string | null;
    state: string | null;
    country: string;

    currentSalary: number;
    expectedSalary: number;

    resumeUrl: string | null;

    firstname: string;
    lastName: string | null;
}

export interface CreateCandidateProfileRequest {
    headline: string | null;
    bio: string | null;

    addressLine1: string | null;
    addressLine2: string | null;

    city: string | null;
    state: string | null;

    country: string;

    currentSalary: number;
    expectedSalary: number;

    resumeUrl: string | null;

    firstname: string;
    lastName: string | null;
}

export interface CandidateSkill {
    id: number;
    skillId: number;
    skillName: string;
    experienceYears: number;
}
export interface CandidateSkillRequest {
    skillId: number;
    experienceYears: number;
}

export interface CandidateEducation {
    id: number;
    instituteName: string;
    degree: string;
    fieldOfStudy?: string | null;
    startYear: number;
    endYear: number;
    percentage: number;
}

export interface CandidateExperience {
    id: number;
    companyName: string;
    designation: string;
    startDate: string;
    endDate?: string;
    currentlyWorking: boolean;
    description?: string;
}

export interface CandidateExperienceRequest {

    companyName: string;
    designation: string;
    startDate: string;
    endDate?: string;
    currentlyWorking: boolean;
    description?: string;

}

export interface CandidateEducationRequest {
    instituteName: string;
    degree: string;
    fieldOfStudy?: string | null;
    startYear: number;
    endYear: number;
    percentage: number;
}