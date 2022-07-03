import AnalysisResultRow from "./analysis-result-row";

export default class ImageAnalysisData {
    analysis_id: number;
    analysis_name: string;
    value_name: String;
    image_url: String;
    notes: String[];
    confidence: Number;
    results: AnalysisResultRow[];
}