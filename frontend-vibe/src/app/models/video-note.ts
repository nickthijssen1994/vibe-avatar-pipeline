export default class VideoNote {
    note: string;
    frame: number;
    moment: string;
    constructor(
        note: string,
        frame: number,
        moment: string) {
        this.note = note;
        this.frame = frame;
        this.moment = moment;
    }
}