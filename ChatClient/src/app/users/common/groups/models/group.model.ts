export class Group {
    id: number;
    ownerId: string;
    userName: string;
    email: string;
    subject: string;
    description: string;
    users: Array<any>;
    isJoined: boolean = false;
}