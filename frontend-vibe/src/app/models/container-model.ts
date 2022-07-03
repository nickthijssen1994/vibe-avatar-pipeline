import AnalysisModel from "./analysis-model";

export default class ContainerModel {
  id: string | null;
  name: string | null;
  analysis: AnalysisModel[] = [];

  constructor(id: string | null = null, name: string | null = null) {
    this.id = id;
    this.name = name;
  }
}
