import { RoleType } from "src/app/enums/role-types";
import { Person } from "./person";
import { VisitorModel } from "./visitor-model";

export class UserModel extends Person {
  public visitorId?: string;
  public visitor?: VisitorModel;
  public role: RoleType;
}
