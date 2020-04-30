export class FriendModel {
    constructor(userName: string, connectionId: string) {
        this.userName = userName;
        this.connectionId = connectionId;
    }
    
    connectionId: string;
    userName: string;
}