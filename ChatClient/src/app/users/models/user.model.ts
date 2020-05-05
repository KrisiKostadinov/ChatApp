export class User {
    userId: string;
    id?: string;
    email: string;
    userName: string;
    city: string;
    age: string;
    country: string;
    education: string;
    highSchool: string;
    job: string;
    previousJob: string;
    skills: [];
    university: string;
    isRequested: boolean = false;
    isFriends?: boolean = false;
}