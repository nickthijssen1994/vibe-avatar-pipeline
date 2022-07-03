import FileModel from "./file-model";

export default class AnalysisModel {
  id: number | null;
  algorithm: string;
  scenario: string;
  files: FileModel[] = [];
  description: string;

  constructor(id: number | null = null, algorithm: string | null = null,
              scenario: string | null = null,
              description: string | null = null) {
    this.id = id;
    this.algorithm = algorithm;
    this.scenario = scenario;
    this.description = description;
  }
}
