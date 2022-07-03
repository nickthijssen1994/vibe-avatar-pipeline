import AnalysisResultRow from "./analysis-result-row";

export default class VideoSection {
    starting_frame: number;
    confidence: Number;
    results: AnalysisResultRow[];

    constructor(
        starting_frame: number) {
        this.starting_frame = starting_frame;
    }
}