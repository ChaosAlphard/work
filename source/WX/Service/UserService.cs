using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WX.Dto;
using WX.Model;

namespace WX.Service {
    interface UserService {
        /**
         * 获取用户信息
         */
        VDto<String> getUserInfo(string code);

        /**
         * 查找所有User信息
         */
        VDto<User> findAll();

        /**
         * 通过Openid查找用户
         * @Param: openid openid
         */
        VDto<User> findByOpenid(string openid);

        /**
         * 用户不存在则插入, 存在则更新
         */
        VDto<User> updateUser(string openid, string name, string avatar);
    }
}
