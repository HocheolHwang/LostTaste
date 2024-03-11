/**
 * ERD의 정의와 ERDCloud의 도메인을 기반으로 엔티티를 일괄 정의하기 위한 기능을 제공합니다.
 * @see https://www.erdcloud.com/d/vgwjPSzzapScQxCEb ERD
 * @author 구본웅
 */

import { Column, CreateDateColumn, DeleteDateColumn, JoinColumn, ManyToOne, ObjectType, PrimaryColumn, PrimaryGeneratedColumn } from "typeorm";
import { CommonCode } from "./entity/common-code";

/**
 * Auto-increment PK 타입에 대한 데코레이터를 제공합니다.
 * @param comment 코멘트
 * @param name 컬럼 이름
 * @returns 
 */
export const GeneratedId = (comment?: string, name?: string) => PrimaryGeneratedColumn({ type: 'bigint', comment, name });

/**
 * PK 타입에 대한 데코레이터를 제공합니다.
 * 
 * 식별 관계에서 복합 키로 포함된 FK를 나타내는데 사용합니다.
 * @param comment 코멘트
 * @returns 
 */
export const Id = (comment?: string, name?: string) => PrimaryColumn({ type: 'bigint', comment, name});

/**
 * 생성시간 컬럼에 대한 데코레이터를 제공합니다.
 * @param comment 코멘트
 * @returns 
 */
export const CreatedAt = (comment?: string) => CreateDateColumn({ type: 'datetime', comment });

/**
 * 공통코드 엔티티와 연관관계를 맺는 컬럼에 대한 데코레이터입니다.
 * 
 * 내부적으로, 코드 테이블과 단방향 참조의 1:N 관계를 매핑합니다. 이 데코레이터가 적용된 컬럼이 연관 관계의 주인(owner)이 됩니다.
 * 
 * NOTE: 만약 컬럼이 코드 테이블에 대해 FK이면서 식별 관계인 경우, 컬럼을 분리하고 `@Id`와 @ManyToOne`을 사용하세요.
 * 
 * @param name 테이블 상에서의 컬럼 이름. 생략하면 참조 테이블의 ID 컬럼명이 snake_case로 변환되어 적용됩니다.
 * @param nullable 해당 컬럼의 Nullable 여부, 기본 false.
 */
export const CodeColumn = (name?: string, nullable: boolean = false) => (
    (target: Object, propertyKey: string | symbol): void => {
        JoinColumn({ name })(target, propertyKey);
        ManyToOne(() => CommonCode, { nullable })(target, propertyKey);
    }
);

/**
 * 삭제 시간 컬럼에 대한 데코레이터를 제공합니다.
 * @param comment 코멘트
 * @returns 
 */
export const DeletedAt = (comment?: string) => DeleteDateColumn({ type: 'datetime', comment })

/**
 * 패스워드 컬럼에 대한 데코레이터를 제공합니다.
 * @param comment 코멘트
 * @returns 
 */
export const Password = (comment?: string) => Column({ type: 'char', length: 64, nullable: true, comment })