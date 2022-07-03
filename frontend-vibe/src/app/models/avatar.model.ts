import AnalysisModel from "./analysis-model";

export default class AvatarModel {
  id: number | null;
  name: string;
  analysis: AnalysisModel[] = [];

  constructor(
    id: number | null = null,
    name: string | null = null
  ) {
    this.id = id;
    this.name = name;
  }
}
