import AnalysisResultRow from "./analysis-result-row";



export default class AnalysisResult {
    fileName: string;
    analysisName: string;
    valueName: string;
    confidence: number;
    notes: string[] = [];
    results: AnalysisResultRow[] = [];
    
  
    constructor(fileName: string | null = null,
                analysisName: string | null = null,
                valueName: string | null = null,
                confidence: number | null = null,
                ) {
      this.fileName = fileName;
      this.analysisName = analysisName;
      this.valueName = valueName;
      this.confidence = confidence;
    }
  }