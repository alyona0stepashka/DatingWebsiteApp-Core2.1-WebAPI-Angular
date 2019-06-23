export class EmojiTab {
    Emoji: string[] = new Array();
    constructor() {
        let index: number;
        for (index = 128512; index < 128587; index++) {
        this.Emoji.push('&#' + index.toString() + ';');
        }
    }
}
