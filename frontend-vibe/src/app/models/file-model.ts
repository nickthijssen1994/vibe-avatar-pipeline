import fileType from "./file-type";

export default class FileModel {
  id: number | null;
  name: string | null;
  fileType: fileType | null;
  downloadLink!: string | null;

  constructor(
    id: number | null = null,
    name: string | null = null,
    fileType: fileType | null = null,
    downloadLink: string | null = null
  ) {
    this.id = id,
      this.name = name,
      this.fileType = fileType,
      this.downloadLink = downloadLink
  }
}
