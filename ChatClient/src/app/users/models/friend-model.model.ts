export class FriendModel {
    constructor(userName: string, connectionId: string) {
        this.userName = userName;
        this.connectionId = connectionId;
    }
    
    userId: string;
    connectionId: string;
    userName: string;
    liveOn: boolean = false;
}