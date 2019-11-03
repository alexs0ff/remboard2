import { IEntityBase } from "../../ra-cruds/ra-cruds.module";

export interface UserEntity extends IEntityBase {
	projectRoleId: number;
	projectRoleTitle: string;
	loginName: string;
	firstName: string;
	lastName: string;
	middleName: string;
	phone: string;
	email: string;
	password: string;
	branches: UserBranchDto[];
}

export interface UserBranchDto {
	branchId: string;
	branchTitle:string;
}
