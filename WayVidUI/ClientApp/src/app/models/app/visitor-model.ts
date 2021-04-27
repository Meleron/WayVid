import { Person } from "./person";
import { UserModel } from "./user-model";

export class VisitorModel extends Person {
  public userId?: string;
  public user?: UserModel;
}
