import { RoleType } from "src/app/enums/role-types";

export class SignUpModel {
  username: string;
  password: string;
  repeatedPassword: string;
  userRole: RoleType;
}
