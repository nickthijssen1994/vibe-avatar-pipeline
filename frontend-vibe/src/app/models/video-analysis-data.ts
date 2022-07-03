import AnalysisResultRow from "./analysis-result-row";
import VideoNote from "./video-note";
import VideoSection from "./video-section";

export default class VideoAnalysisData {
    analysis_id: number;
    name: string;
    value_name: String;
    image_url: String;
    notes: VideoNote[];
    sections: VideoSection[];
}