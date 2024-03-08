import { Injectable, Logger, OnApplicationBootstrap } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { CustomCode } from 'src/db/entity/custom-code';
import { Repository } from 'typeorm';
import { CodeMap, CustomCodeMap, PrefixAndCode } from './code.type';
import { ItemCode } from 'src/db/entity/item-code';
import { CustomCodeType } from 'src/db/entity/custom-code-type';

@Injectable()
export class CodeService implements OnApplicationBootstrap {
    private readonly logger: Logger = new Logger(CodeService.name);

    constructor (
        @InjectRepository(CustomCodeType)
        private customCodeTypeRepository: Repository<CustomCodeType>,

        @InjectRepository(CustomCode)
        private customCodeRepository: Repository<CustomCode>,

        @InjectRepository(ItemCode)
        private itemCodeRepository: Repository<ItemCode>,
    ) {}
    
    private codeMap: CodeMap = {
        custom: new Map(),
        item: new Map()
    };

    private readonly CODE_PATTERN: RegExp = /^\[A-Za-z]{3}_\d{4}$/g;

    /**
     * Nest 부팅 시 실행되는 함수
     */
    async onApplicationBootstrap(): Promise<void> {
        const custom = this.codeMap.custom;

        const customCodes: CustomCode[] = await this.customCodeRepository.find();

        customCodes.forEach((customCode) => {
            const prefix = customCode.type.prefix;
            if (custom.has(prefix)) {
                custom.set(prefix, new Map() as CustomCodeMap);
            }
            else {
                custom.get(prefix).set(customCode.id, customCode.description)
            }
        });
        
        const itemCodes: ItemCode[] = await this.itemCodeRepository.find();

        itemCodes.forEach((itemCode) => {
            this.codeMap.item.set(itemCode.itemCode, itemCode.itemName);
        });

        this.logger.log('공통 코드 로딩 완료!');
    }

    async getCustomCodeTypeEntity(prefix: string): Promise<CustomCodeType | undefined> {
        return await this.customCodeTypeRepository.findOne({where: {prefix} });
    }

    async getCustomCodeEntity(fullCode: string | PrefixAndCode): Promise<CustomCode | undefined> {
        let prefix = '';
        let code = '';
        if (typeof fullCode === 'string') {
            if (!this.CODE_PATTERN.test(fullCode)) {
                throw new Error('잘못된 양식의 커스텀 코드입니다.');
            }
    
            const matchArray: RegExpMatchArray = fullCode.match(this.CODE_PATTERN);
            prefix = matchArray[1];
            code = matchArray[2];
        }
        else {
            prefix = fullCode.prefix;
            code = fullCode.code;
        }
        
        return await this.customCodeRepository.findOne({
            where: {type: {prefix: prefix}, id: code}
        });
    }
}
