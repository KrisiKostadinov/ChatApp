export class MessageModel {
    constructor(userName: string = null, content: string = null) {
        this.userName = userName;
        this.content = content;
    }

    content: string;
    userName: string;
}