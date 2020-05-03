export class MessageModel {
    constructor(
        receiverUserName: string,
        senderUserName: string,
        receiverId: string,
        senderId: string,
        content: string) {
            this.receiverUserName = receiverUserName;
            this.senderUserName = senderUserName;
            this.receiverId = receiverId;
            this.senderId = senderId;
            this.content = content;
    }

    content: string;
    receiverUserName: string;
    receiverId: string;
    senderUserName: string;
    senderId: string;
}