import { Column, Entity, JoinColumn, ManyToOne, PrimaryColumn } from "typeorm";
import { CommonCodeType } from "./common-code-type";

@Entity({ comment: '공통 코드' })
export class CommonCode {
    // @Id('공통 코드 ID', 'common_code_id')   // TODO: why (0, typeorm_utils_1.Id) is not a function??
    @PrimaryColumn({
        type: 'bigint',
        name: 'common_code_id',
        comment: '공통 코드 ID',
    })
    id: string;

    @ManyToOne(() => CommonCodeType, { eager: true })
    @JoinColumn({
        name: 'type_id',
    })
    type: CommonCodeType;

    @Column({
        type: 'varchar',
        length: 16,
        comment: '공통 코드 설명'
    })
    description: string;
}