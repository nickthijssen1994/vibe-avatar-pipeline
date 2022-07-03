
export default class AnalysisResultRow {
    valueName: string;
    content: number;

    constructor(valueName: string,
        content: number) {
        this.valueName = valueName;
        this.content = content
    }
}