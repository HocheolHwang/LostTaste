import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';

import * as bcrypt from 'bcrypt';
import { CodeService } from 'src/code/code.service';
import { Member } from 'src/db/entity/member';
import { MemberEquipment } from 'src/db/entity/member-equipment';
import { SignupDto } from 'src/user/dto/signup.dto';
import { Repository } from 'typeorm';
import { UserProfileDto } from './dto/user-profile.dto';
import { UserDto } from './dto/user.dto';

@Injectable()
export class UserService {
    constructor (
        @InjectRepository(Member)
        private readonly memberRepository: Repository<Member>,

        private readonly codeService: CodeService
    ) {}
    
    private readonly HASH_SALT_ROUND = 10;

    private readonly DEFAULT_EQUIPMENTS: string[] = ['SKN_0001', 'JOB_0001', 'PET_0001', 'CSK_0001'];

    async findByAccountId(username: string): Promise<Member | undefined> {
        return this.memberRepository.findOne({ where: { accountId: username } });
    }

    async findByDto(dto: UserDto): Promise<Member | undefined> {
        return this.findByAccountId(dto.accountId);
    }

    async signup(dto: SignupDto): Promise<void> {
        const member: Member = await this.memberRepository.save({
            accountId: dto.accountId,
            password: await this.hash(dto.password),
            nickname: dto.nickname,
        });
        
        member.equipments = [];
        for (const codeId of this.DEFAULT_EQUIPMENTS) {
            const memberEquipment = new MemberEquipment();
            memberEquipment.member = member;
            memberEquipment.customCode = this.codeService.getCommonCodeEntity(codeId);
            memberEquipment.customCodeTypeId = memberEquipment.customCode.type.id;
            
            member.equipments.push(memberEquipment);
        }

        this.memberRepository.save(member);
    }

    async hash(plaintext: string) {
        return await bcrypt.hash(plaintext, this.HASH_SALT_ROUND);
    }

    async getProfile(user: UserDto): Promise<UserProfileDto> {
        const entity: Member = await this.findByDto(user);

        const customMap = new Map<string, string>();
        entity.equipments.forEach(equipment => {
            const typeId = equipment.customCodeTypeId;
            const id = equipment.customCode.id;

            customMap.set(typeId, id);
        });

        return {
            ...user,
            lastCustom: customMap
        };
    }
}
