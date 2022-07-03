

export default class AnalysisInfo {
  avatarName: string;
  algorithm: string;
  scenario: string;


  constructor(avatarName: string | null = null,
                algorithm: string | null = null,
              scenario: string | null = null,
              ) {
    this.avatarName = avatarName;
    this.algorithm = algorithm;
    this.scenario = scenario;
  }
}