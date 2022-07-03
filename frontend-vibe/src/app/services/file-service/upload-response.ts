export interface UploadResponse {
  message: string;
  result: FileDescription[];
}

export interface FileDescription {
  avatarName: string;
  containerId: string;
  fileName: string;
  fileUri: string;
}
