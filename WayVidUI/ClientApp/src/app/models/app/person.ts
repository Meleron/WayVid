export abstract class Person {
  public id: string;
  public userName: string;
  public isActive: boolean;
  public createdOn: Date;
  public updatedOn?: Date;
  public deletedOn?: Date;
  public createdBy?: string;
  public updatedBy?: string;
  public deletedBy?: string;
}
