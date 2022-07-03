import errorType from "./error-type";

export default class VideoErrors {
    id: string | null;
    errorType: errorType;
    percent: number;
    avatarId: string;

    constructor(
        id: string | null = null,
        errorType: errorType.Clipping,
        percent: number = 0,
        avatarId: string = ""
    ) {
        this.id = id;
        this.errorType = errorType;
        this.percent = percent;
        this.avatarId = avatarId;
    }
}
