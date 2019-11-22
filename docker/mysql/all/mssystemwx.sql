CREATE DATABASE /*!32312 IF NOT EXISTS*/`mssystemwx` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `mssystemwx`;

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;




-- ----------------------------
-- Table structure for wx_account
-- ----------------------------
DROP TABLE IF EXISTS `wx_account`;
CREATE TABLE `wx_account`  (
  `WeixinId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '微信原始Id',
  `AppId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '开发者ID',
  `AppSecret` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '开发者密码',
  `WeixinName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '公众号名称',
  `WeixinQRCode` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '公众号二维码地址',
  `AccessToken` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'AccessToken',
  `AccessTokenCreateTime` datetime(0) NULL DEFAULT NULL COMMENT 'AccessToken创建时间',
  `JsApiTicket` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'JS API临时票据',
  `JsApiTicketCreateTime` datetime(0) NULL DEFAULT NULL COMMENT 'JS API临时票据创建时间',
  `SubscribePageUrl` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信号关注引导页地址URL',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `Token` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '令牌',
  `EncodingAESKey` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '消息加解密密钥',
  PRIMARY KEY (`WeixinId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '公众号表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wx_account
-- ----------------------------
INSERT INTO `wx_account` VALUES ('gh_3f66fb647ff1', 'wxeb8c08a03de853d5', '485c8b1eeb798401c9af06798f91ec6c', '自由reading', NULL, '17_ESnucPG7Ccpi7_t7AfPIUMzUN2v9Wi2uDYlsJRyh4Ho7bivZh9AmoIkXjTXZYUKcTyP1IOo7WifJJiyjnRfAnRaDpyv0zYO4OFiSAUYLGF-pVAAV5vZcDREipUIole9JwQVmH07mOo7yKO6NUOViABALFM', '2019-01-12 10:29:53', NULL, NULL, NULL, 1546912897, 'wangmaosheng', 'w9jBowtJ9rqgzcHFFyJ0VakWDdNfknphEmFB7pocglk');

-- ----------------------------
-- Table structure for wx_account_miniprogram
-- ----------------------------
DROP TABLE IF EXISTS `wx_account_miniprogram`;
CREATE TABLE `wx_account_miniprogram`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `WeixinId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '微信原始Id',
  `AppId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'AppID(小程序ID)',
  `AppSecret` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'AppSecret(小程序密钥)',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `WeixinId`(`WeixinId`) USING BTREE,
  CONSTRAINT `wx_account_miniprogram_ibfk_1` FOREIGN KEY (`WeixinId`) REFERENCES `wx_account` (`WeixinId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wx_account_miniprogram
-- ----------------------------
INSERT INTO `wx_account_miniprogram` VALUES (1, 'gh_3f66fb647ff1', 'wx20a06c30c8b81e61', '6223a1e06f10ebc8ae9fe9eed65042bb');

-- ----------------------------
-- Table structure for wx_keyword
-- ----------------------------
DROP TABLE IF EXISTS `wx_keyword`;
CREATE TABLE `wx_keyword`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleId` int(11) NOT NULL COMMENT '规则表ID',
  `Keyword` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '关键字',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `RuleId`(`RuleId`) USING BTREE,
  CONSTRAINT `wx_keyword_ibfk_1` FOREIGN KEY (`RuleId`) REFERENCES `wx_rule` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '关键字表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wx_keyword
-- ----------------------------
INSERT INTO `wx_keyword` VALUES (1, 3, '妹子', 1560356237);

-- ----------------------------
-- Table structure for wx_menu
-- ----------------------------
DROP TABLE IF EXISTS `wx_menu`;
CREATE TABLE `wx_menu`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ParentId` int(11) NOT NULL DEFAULT 0 COMMENT '父级ID',
  `Name` varchar(60) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '菜单标题，不超过16个字节，子菜单不超过60个字节',
  `Type` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单的响应动作类型，view表示网页类型，click表示点击类型，miniprogram表示小程序类型',
  `Key` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单KEY值，用于消息接口推送，不超过128字节',
  `Url` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '网页 链接，用户点击菜单可打开链接，不超过1024字节。 type为miniprogram时，不支持小程序的老版本客户端将打开本url。',
  `AppId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '小程序的appid（仅认证公众号可配置）',
  `PagePath` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '小程序的页面路径',
  `Sort` int(11) NOT NULL DEFAULT 0,
  `IsDel` int(11) NOT NULL DEFAULT 0,
  `CreateTime` bigint(20) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '微信菜单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wx_miniprogram_user
-- ----------------------------
DROP TABLE IF EXISTS `wx_miniprogram_user`;
CREATE TABLE `wx_miniprogram_user`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `OpenId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '小程序对应该用户的OpenId',
  `UnionId` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信开放平台的唯一标识符',
  `NickName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '昵称',
  `City` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户所在城市',
  `Country` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户所在国家',
  `Province` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户所在的省份',
  `Language` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '语言',
  `Gender` tinyint(4) NULL DEFAULT NULL COMMENT '性别',
  `AvatarUrl` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '头像',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`OpenId`, `Id`) USING BTREE,
  INDEX `Id`(`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '小程序用户表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wx_miniprogram_user
-- ----------------------------
INSERT INTO `wx_miniprogram_user` VALUES (1, 'ofovX1PhIU2fMvagdO1wtJj2U8Bc', NULL, '岁月静好', 'Lianyungang', 'China', 'Jiangsu', 'zh_CN', 1, 'https://wx.qlogo.cn/mmopen/vi_32/OmJum4poEd64ibQjFZ4DWqND7OtN2ia9akuYSn9fxWrwT4PaH71mqr7ls3jdYHDlj10Oq8vS9pjw27WjebgMYWWA/132', 1555578279);

-- ----------------------------
-- Table structure for wx_news_response
-- ----------------------------
DROP TABLE IF EXISTS `wx_news_response`;
CREATE TABLE `wx_news_response`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleId` int(11) NOT NULL COMMENT '规则ID',
  `Title` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图文消息标题',
  `Description` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图文消息描述',
  `PicUrl` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200',
  `Url` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '点击图文消息跳转链接',
  `Sort` int(11) NOT NULL DEFAULT 0 COMMENT '排序',
  `IsDel` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否已删除0否，1是',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `RuleId`(`RuleId`) USING BTREE,
  CONSTRAINT `wx_news_response_ibfk_1` FOREIGN KEY (`RuleId`) REFERENCES `wx_rule` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '回复图文消息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wx_rule
-- ----------------------------
DROP TABLE IF EXISTS `wx_rule`;
CREATE TABLE `wx_rule`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '规则名称',
  `RuleType` tinyint(4) NOT NULL COMMENT '规则类型0:普通，1:未匹配到回复规则',
  `RequestMsgType` int(11) NOT NULL COMMENT '规则类型',
  `ResponseMsgType` int(11) NOT NULL COMMENT '响应消息类型',
  `CreateTime` bigint(20) NOT NULL COMMENT '规则创建时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '响应微信消息规则' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wx_rule
-- ----------------------------
INSERT INTO `wx_rule` VALUES (1, '自动回复', 2, 1, 1, 1547103227);
INSERT INTO `wx_rule` VALUES (2, '关注回复', 1, 8, 1, 1547112830);
INSERT INTO `wx_rule` VALUES (3, '求妹子', 0, 1, 1, 1560356237);

-- ----------------------------
-- Table structure for wx_text_response
-- ----------------------------
DROP TABLE IF EXISTS `wx_text_response`;
CREATE TABLE `wx_text_response`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleId` int(11) NOT NULL COMMENT '规则ID',
  `Content` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '消息内容',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `RuleId`(`RuleId`) USING BTREE,
  CONSTRAINT `wx_text_response_ibfk_1` FOREIGN KEY (`RuleId`) REFERENCES `wx_rule` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '文本回复' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wx_text_response
-- ----------------------------
INSERT INTO `wx_text_response` VALUES (1, 1, '亲，您发的我识别不了啊', 1547103227);
INSERT INTO `wx_text_response` VALUES (2, 2, '终于等到你了~~', 1547112831);
INSERT INTO `wx_text_response` VALUES (3, 3, '我就是', 1560356237);

-- ----------------------------
-- Table structure for wx_user
-- ----------------------------
DROP TABLE IF EXISTS `wx_user`;
CREATE TABLE `wx_user`  (
  `OpenId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '用户OpenId主键',
  `Subscribe` int(11) NULL DEFAULT NULL COMMENT '用户是否关注该公众号1：关注了，0：没关注',
  `UserName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `NickName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '昵称',
  `Mobile` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '手机号码',
  `Sex` int(11) NULL DEFAULT NULL COMMENT '用户的性别，值为1时是男性，值为2时是女性，值为0时是未知',
  `City` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户所在城市',
  `Country` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户所在国家',
  `Province` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户所在的省份',
  `Headimgurl` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户头像',
  `Birthday` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '生日',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `IsSync` int(11) NOT NULL DEFAULT 0 COMMENT '用户基本信息是否同步过',
  PRIMARY KEY (`OpenId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '微信用户表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wx_user
-- ----------------------------
INSERT INTO `wx_user` VALUES ('ouUD4sr2rk1MT2UqTTqpGNFot-GI', 1, NULL, '不覊之士', NULL, 1, '连云港', '中国', '江苏', 'http://thirdwx.qlogo.cn/mmopen/eDicfvb2fvAYRduibVLeexKc0ZdJ67jryCbibbwWwTaypS2G5rdajCzRzq4QKbYVazuoe2Z9VTNlTzB6O6xOSep0cB24cfgPOXQ/132', NULL, 1547123099, 1);

SET FOREIGN_KEY_CHECKS = 1;
