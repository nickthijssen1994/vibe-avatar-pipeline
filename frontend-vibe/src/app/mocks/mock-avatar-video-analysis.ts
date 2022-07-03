import AvatarAnalysisModel from "src/app/models/video-errors";
import errorType from "src/app/models/error-type";
import VideoErrors from "src/app/models/video-errors";

export const VIDEOERRORS: VideoErrors[] = [
    { id: '0', time: '0:50', errorType: errorType.Clipping, percent: 85, avatarId: '0' },
    { id: '1', time: '2:36', errorType: errorType.Clipping, percent: 70, avatarId: '0' },
    { id: '2', time: '0:32', errorType: errorType.Emotion, percent: 70, avatarId: '1' },
    { id: '3', time: '3:50', errorType: errorType.Clipping, percent: 70, avatarId: '1' },
  ];
