import AvatarAnalysisModel from "src/app/models/video-errors";
import errorType from "src/app/models/error-type";
import ImageErrors from "src/app/models/image-errors";

export const IMAGEERRORS: ImageErrors[] = [
    { id: '0', errorType: errorType.Clipping, percent: 85, avatarId: '0' },
    { id: '1', errorType: errorType.Clipping, percent: 70, avatarId: '0' },
    { id: '2', errorType: errorType.Emotion, percent: 70, avatarId: '1' },
    { id: '3', errorType: errorType.Clipping, percent: 70, avatarId: '1' },
  ];
