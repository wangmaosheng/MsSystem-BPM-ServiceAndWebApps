/*
SQLyog Professional v12.08 (64 bit)
MySQL - 5.7.19-log : Database - mssystem
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`mssystem` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `mssystem`;

/*Table structure for table `cap.published` */

DROP TABLE IF EXISTS `cap.published`;

CREATE TABLE `cap.published` (
  `Id` bigint(20) NOT NULL,
  `Version` varchar(20) DEFAULT NULL,
  `Name` varchar(200) NOT NULL,
  `Content` longtext,
  `Retries` int(11) DEFAULT NULL,
  `Added` datetime NOT NULL,
  `ExpiresAt` datetime DEFAULT NULL,
  `StatusName` varchar(40) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*Data for the table `cap.published` */

/*Table structure for table `cap.received` */

DROP TABLE IF EXISTS `cap.received`;

CREATE TABLE `cap.received` (
  `Id` bigint(20) NOT NULL,
  `Version` varchar(20) DEFAULT NULL,
  `Name` varchar(400) NOT NULL,
  `Group` varchar(200) DEFAULT NULL,
  `Content` longtext,
  `Retries` int(11) DEFAULT NULL,
  `Added` datetime NOT NULL,
  `ExpiresAt` datetime DEFAULT NULL,
  `StatusName` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*Data for the table `cap.received` */

/*Table structure for table `oa_chat` */

DROP TABLE IF EXISTS `oa_chat`;

CREATE TABLE `oa_chat` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Sender` bigint(20) NOT NULL COMMENT '发送方',
  `Message` text NOT NULL COMMENT '消息',
  `Receiver` bigint(20) NOT NULL COMMENT '接收方',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=144 DEFAULT CHARSET=utf8;

/*Data for the table `oa_chat` */

insert  into `oa_chat`(`Id`,`Sender`,`Message`,`Receiver`,`CreateTime`) values (1,5,'你好',1,1560898931),(2,1,'你好',5,1560898950),(3,5,'你好',1,1560900023),(4,5,'你好',1,1560900408),(5,1,'你好',5,1561000203),(6,5,'你好',1,1561000218),(7,1,'你好',5,1561000544),(8,1,'你好',5,1561000556),(9,1,'你好',5,1561001438),(10,1,'你好',5,1561001453),(11,1,'你好',5,1561001465),(12,1,'你好',5,1561001531),(13,1,' 你好',5,1561001613),(14,1,'你好',5,1561001632),(15,1,'额嗯',5,1561001635),(16,5,'你好',1,1561001642),(17,1,'我也好',5,1561001648),(18,1,'123',5,1561001955),(19,1,'老板，在吗》',5,1561001991),(20,1,'我想涨工资',5,1561001996),(21,5,'你白天没睡醒？？？',1,1561002015),(22,1,'老板我想看看外面的世界',5,1561002033),(23,5,'外面的世界很危险',1,1561002047),(24,1,'你要想清楚',5,1561002226),(25,1,'你好',5,1561002776),(26,1,'么么哒',5,1561002780),(27,1,'你好',5,1561002792),(28,1,'在吗',5,1561002796),(29,1,'我找你有事情',5,1561002805),(30,1,'你好',5,1561003219),(31,1,'你好',5,1561003221),(32,1,'你好',5,1561003223),(33,1,'在不',5,1561003229),(34,1,'我找你有事情',5,1561003235),(35,5,'你说',1,1561003243),(36,1,'在不',5,1561003406),(37,1,'在不',5,1561003406),(38,1,'',5,1561003407),(39,1,'在不',5,1561003407),(40,1,'在不',5,1561003408),(41,1,'在不',5,1561003408),(42,1,'在不',5,1561003409),(43,1,'在不',5,1561003420),(44,1,'？？',5,1561003425),(45,1,'在不',5,1561003472),(46,1,'在不',5,1561003476),(47,1,'',5,1561003476),(48,1,'在不',5,1561003477),(49,1,'在不',5,1561003477),(50,1,'在不',5,1561003478),(51,1,'在不？',5,1561003497),(52,1,'在不？',5,1561003498),(53,1,'在不？',5,1561003499),(54,1,'zaibu',5,1561003541),(55,1,'在不',5,1561003543),(56,1,'在不',5,1561003556),(57,5,'在的',1,1561003560),(58,5,'有事情吗',1,1561003564),(59,1,'领导，我想涨工资',5,1561003572),(60,5,'你昨天晚上没睡好吧？',1,1561003589),(61,5,'你明天休息一天吧',1,1561003608),(62,1,'老板，我想看看外面的世界',5,1561003622),(63,4,'妹子在不？',5,1561003789),(64,5,'滚',4,1561003816),(65,1,'在吗？',4,1561014686),(66,1,'在吗？',4,1561014711),(67,1,'在吗？',4,1561014753),(68,1,'在吗？',4,1561014834),(69,1,'在吗？',4,1561014928),(70,1,'你好？',4,1561014999),(71,1,'在吗？',4,1561015027),(72,1,'在吗？',4,1561015106),(73,1,'在吗？我找你有事情！！！',4,1561015154),(74,6,'在吗？领导找你了',4,1561015195),(75,4,'在吗？',1,1561015241),(76,1,'在吗？',4,1561015256),(77,1,'我找你有事情',4,1561015260),(78,6,'在吗？领导找你了',4,1561015278),(79,6,'啊啊',4,1561015290),(80,6,'在吗？',4,1561015340),(81,1,'你过来下',4,1561015379),(82,1,'你过来下',4,1561015403),(83,6,'领导找你了，快点过去',4,1561015423),(84,1,'在吗？',4,1561015485),(85,6,'领导找你了，快点过去',4,1561015501),(86,4,'在的',1,1561015515),(87,4,'好的，我马上过去',6,1561015521),(88,1,'ok',4,1561015530),(89,6,'ok',4,1561015534),(90,1,'在吗？',4,1561019956),(91,4,'我在的',1,1561019974),(92,1,'你来办公室一下',4,1561019988),(93,4,'好的，稍等',1,1561019994),(94,1,'在吗？',5,1561020009),(95,1,'你来下',5,1561020011),(96,1,'在？',5,1561020024),(97,1,'在？',5,1561020057),(98,1,'在？',5,1561020095),(99,1,'在？',5,1561020132),(100,1,'在？',5,1561020205),(101,1,'咋',5,1561020366),(102,5,'aa',1,1561020375),(103,1,'在吗？',5,1561024161),(104,5,'在的',1,1561024170),(105,5,'有事情吗?',1,1561024175),(106,1,'在吗？',6,1561024186),(107,1,'在吗？',6,1561024199),(108,1,'在吗？',4,1561024210),(109,4,'在de ',1,1561024216),(110,1,'你好',4,1561025712),(111,1,'在吗',4,1561025721),(112,1,'在吗',4,1561026638),(113,4,'在的',1,1561026646),(114,4,'请说',1,1561026650),(115,1,'我想离职',4,1561026655),(116,4,'那个家伙想离职',5,1561026665),(117,4,'你看看什么情况',5,1561026670),(118,4,'尽量工资涨少些',5,1561026686),(119,4,'知道吗？',5,1561026699),(120,1,'在吗？',5,1561038348),(121,5,'在的',1,1561038357),(122,5,'有事情吗？',1,1561038362),(123,1,'嗯嗯',5,1561038367),(124,1,'你来办公室一下',5,1561038372),(125,1,'我找你谈谈最近工作情况',5,1561038382),(126,5,'好的，我马上到',1,1561038398),(127,1,'在吗',5,1561038823),(128,5,'在的',1,1561038826),(129,5,'你说',1,1561038833),(130,1,'你来办公室一下',5,1561038841),(131,5,'出来',1,1561038934),(132,1,'e',5,1561038943),(133,1,'额',5,1561038945),(134,1,'有屁快放',5,1561038950),(135,5,'我靠',1,1561038961),(136,5,'敢这样说话？？？',1,1561038967),(137,5,'你确定',1,1561038969),(138,5,'不想活了',1,1561038972),(139,5,'FUCJ',1,1561038975),(140,5,'FUCK',1,1561038977),(141,1,'123',5,1561039879),(142,5,'FUCK',1,1561044044),(143,1,'你比了啊你',5,1561044052);

/*Table structure for table `oa_leave` */

DROP TABLE IF EXISTS `oa_leave`;

CREATE TABLE `oa_leave` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `LeaveCode` varchar(30) NOT NULL COMMENT '请假编号',
  `Title` varchar(500) DEFAULT NULL COMMENT '标题',
  `UserId` int(11) NOT NULL COMMENT '请假人',
  `AgentId` int(11) NOT NULL COMMENT '工作代理人',
  `LeaveType` tinyint(4) NOT NULL COMMENT '请假类型',
  `Reason` varchar(1000) DEFAULT NULL COMMENT '请假原因',
  `Days` int(11) NOT NULL COMMENT '请假天数',
  `StartTime` bigint(20) NOT NULL COMMENT '开始时间',
  `EndTime` bigint(20) NOT NULL COMMENT '结束时间',
  `CreateUserId` int(11) NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `FlowStatus` int(11) DEFAULT '-1' COMMENT '流程状态',
  `FlowTime` bigint(20) DEFAULT NULL COMMENT '流程操作时间戳',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='员工请假';

/*Data for the table `oa_leave` */

insert  into `oa_leave`(`Id`,`LeaveCode`,`Title`,`UserId`,`AgentId`,`LeaveType`,`Reason`,`Days`,`StartTime`,`EndTime`,`CreateUserId`,`CreateTime`,`FlowStatus`,`FlowTime`) values (1,'15604170854388','想看看外面的世界',0,0,0,'想看看外面的世界',7,1560441600,1560960000,1,1560417085,1,1560417476),(2,'15604180604164','测试2',0,0,0,'测试',2,1560441600,1560528000,1,1560418060,1,1560419404);

/*Table structure for table `oa_mail` */

DROP TABLE IF EXISTS `oa_mail`;

CREATE TABLE `oa_mail` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Title` varchar(100) NOT NULL,
  `Content` varchar(200) NOT NULL,
  `SendStatus` tinyint(4) NOT NULL COMMENT '发送状态',
  `Sender` varchar(20) NOT NULL COMMENT '发送人',
  `SendMail` varchar(50) DEFAULT NULL COMMENT '发送地址',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `oa_mail` */

/*Table structure for table `oa_mail_config` */

DROP TABLE IF EXISTS `oa_mail_config`;

CREATE TABLE `oa_mail_config` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Host` varchar(50) NOT NULL COMMENT '邮件服务地址',
  `Port` int(11) NOT NULL COMMENT '端口',
  `SecureSocketOptions` tinyint(4) NOT NULL DEFAULT '1' COMMENT 'Secure socket options.',
  `UserName` varchar(50) NOT NULL COMMENT '账号',
  `Password` varchar(50) NOT NULL COMMENT '密码',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='邮件服务配置';

/*Data for the table `oa_mail_config` */

insert  into `oa_mail_config`(`Id`,`Host`,`Port`,`SecureSocketOptions`,`UserName`,`Password`) values (1,'	smtp.qq.com',587,1,'2636256005@qq.com','snewsyiqgyagecdd');

/*Table structure for table `oa_message` */

DROP TABLE IF EXISTS `oa_message`;

CREATE TABLE `oa_message` (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MsgType` int(11) NOT NULL COMMENT '消息类型',
  `FaceUserType` tinyint(4) NOT NULL COMMENT '面向人员类型',
  `Title` varchar(200) DEFAULT NULL COMMENT '标题',
  `IsLocal` tinyint(4) NOT NULL COMMENT '是否是本地消息',
  `TargetType` varchar(50) NOT NULL COMMENT '跳转方式',
  `Link` text COMMENT '链接地址',
  `Content` text COMMENT '消息内容',
  `IsEnable` tinyint(4) NOT NULL COMMENT '是否立即生效',
  `StartTime` bigint(20) DEFAULT NULL COMMENT '开始时间',
  `EndTime` bigint(20) DEFAULT NULL COMMENT '结束时间',
  `IsExecuted` tinyint(4) NOT NULL COMMENT '是否执行过',
  `IsDel` tinyint(4) NOT NULL COMMENT '是否删除',
  `MakerUserId` bigint(20) DEFAULT NULL COMMENT '编制人ID',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人Id',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='系统消息表';

/*Data for the table `oa_message` */

insert  into `oa_message`(`Id`,`MsgType`,`FaceUserType`,`Title`,`IsLocal`,`TargetType`,`Link`,`Content`,`IsEnable`,`StartTime`,`EndTime`,`IsExecuted`,`IsDel`,`MakerUserId`,`CreateUserId`,`CreateTime`) values (1,1,0,'测试',1,'tab',NULL,'测试',1,1558945580,1558945580,1,0,0,1,1555213291),(2,0,0,'测试2',1,'blank',NULL,NULL,1,0,0,1,0,0,1,1555315893);

/*Table structure for table `oa_message_user` */

DROP TABLE IF EXISTS `oa_message_user`;

CREATE TABLE `oa_message_user` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MessageId` bigint(20) NOT NULL COMMENT '消息ID',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  PRIMARY KEY (`Id`),
  KEY `MessageId` (`MessageId`),
  CONSTRAINT `oa_message_user_ibfk_1` FOREIGN KEY (`MessageId`) REFERENCES `oa_message` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='消息用户关系表';

/*Data for the table `oa_message_user` */

/*Table structure for table `oa_message_user_read` */

DROP TABLE IF EXISTS `oa_message_user_read`;

CREATE TABLE `oa_message_user_read` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MessageId` bigint(20) NOT NULL COMMENT '消息ID',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  PRIMARY KEY (`Id`),
  KEY `MessageId` (`MessageId`),
  CONSTRAINT `oa_message_user_read_ibfk_1` FOREIGN KEY (`MessageId`) REFERENCES `oa_message` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='用户消息已读表';

/*Data for the table `oa_message_user_read` */

insert  into `oa_message_user_read`(`Id`,`MessageId`,`UserId`) values (1,1,1);

/*Table structure for table `oa_work` */

DROP TABLE IF EXISTS `oa_work`;

CREATE TABLE `oa_work` (
  `WorkId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `WorkType` tinyint(4) NOT NULL COMMENT '类型：1日报，2周报，3月报',
  `Content` varchar(1000) DEFAULT NULL COMMENT '已完成工作',
  `PlanContent` varchar(1000) DEFAULT NULL COMMENT '计划完成工作',
  `NeedHelpContent` varchar(1000) DEFAULT NULL COMMENT '需要协助的工作',
  `Memo` varchar(1000) DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除',
  `ReportDate` bigint(20) NOT NULL COMMENT '汇报日期',
  `CreateUserId` int(11) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`WorkId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='我的工作';

/*Data for the table `oa_work` */

insert  into `oa_work`(`WorkId`,`WorkType`,`Content`,`PlanContent`,`NeedHelpContent`,`Memo`,`IsDel`,`ReportDate`,`CreateUserId`,`CreateTime`) values (1,0,'123','123','123',NULL,0,0,1,0),(2,0,NULL,NULL,NULL,NULL,0,0,1,0);

/*Table structure for table `oa_work_reporter` */

DROP TABLE IF EXISTS `oa_work_reporter`;

CREATE TABLE `oa_work_reporter` (
  `ReportId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `WorkId` int(11) NOT NULL COMMENT '工作id',
  `Reporter` int(11) NOT NULL COMMENT '汇报人',
  `ReadDate` bigint(20) DEFAULT NULL COMMENT '阅读时间',
  `Memo` varchar(500) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`ReportId`),
  KEY `WorkId` (`WorkId`),
  CONSTRAINT `oa_work_reporter_ibfk_1` FOREIGN KEY (`WorkId`) REFERENCES `oa_work` (`WorkId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='汇总人列表';

/*Data for the table `oa_work_reporter` */

/*Table structure for table `oa_workflowsql` */

DROP TABLE IF EXISTS `oa_workflowsql`;

CREATE TABLE `oa_workflowsql` (
  `Name` varchar(50) NOT NULL COMMENT '流程sql名称,必须是以oa_为开头，用于判断属于哪个系统，方便调用接口',
  `FlowSQL` text NOT NULL COMMENT '流程SQL，执行结果必须是一行一列',
  `Param` varchar(200) DEFAULT NULL COMMENT '参数名。以英文 , 分割',
  `SQLType` tinyint(4) NOT NULL DEFAULT '0' COMMENT '类型，0：选人节点，必须返回的是用户ID，1：连线条件，条件通过返回的是一行一列的数据，不通过没有任何返回结果',
  `Status` int(11) NOT NULL DEFAULT '1' COMMENT '状态',
  `Remark` varchar(200) DEFAULT NULL COMMENT '备注',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  PRIMARY KEY (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用于工作流获取权限系统数据';

/*Data for the table `oa_workflowsql` */

insert  into `oa_workflowsql`(`Name`,`FlowSQL`,`Param`,`SQLType`,`Status`,`Remark`,`CreateTime`,`CreateUserId`) values ('oa_leaveLessThenThreeDays','SELECT ol.`Id` FROM `oa_leave` ol WHERE ol.`Days`<=3 AND ol.`CreateUserId`=@userid AND ol.`Id`=@formid','userid,formid',1,1,'请假时间小于等于三天判断',1,1),('oa_leaveMoreThenThreeDays','SELECT ol.`Id` FROM `oa_leave` ol WHERE ol.`Days` > 3 AND ol.`CreateUserId`=@userid AND ol.`Id`=@formid','userid,formid',1,1,'请假时间大于三天判断',1,1);

/*Table structure for table `schedule` */

DROP TABLE IF EXISTS `schedule`;

CREATE TABLE `schedule` (
  `JobId` bigint(20) NOT NULL AUTO_INCREMENT,
  `JobName` varchar(100) NOT NULL,
  `JobGroup` varchar(100) NOT NULL,
  `JobStatus` tinyint(4) NOT NULL,
  `TriggerType` tinyint(4) NOT NULL,
  `Cron` varchar(50) DEFAULT NULL,
  `AssemblyName` varchar(100) DEFAULT NULL,
  `ClassName` varchar(50) DEFAULT NULL,
  `Remark` varchar(500) DEFAULT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `RunTimes` int(11) NOT NULL DEFAULT '0',
  `BeginTime` datetime NOT NULL,
  `EndTime` datetime DEFAULT NULL,
  `IntervalSecond` int(11) DEFAULT NULL,
  `Url` varchar(1000) DEFAULT NULL,
  `Status` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`JobId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

/*Data for the table `schedule` */

insert  into `schedule`(`JobId`,`JobName`,`JobGroup`,`JobStatus`,`TriggerType`,`Cron`,`AssemblyName`,`ClassName`,`Remark`,`CreateTime`,`UpdateTime`,`RunTimes`,`BeginTime`,`EndTime`,`IntervalSecond`,`Url`,`Status`) values (1,'微信AccessToken自动同步任务','DefaultJob',0,0,'3 * * * * ? ','MsSystem.Schedule.Job','WxAccessTokenJob',NULL,'2019-04-17 11:05:13',NULL,0,'2019-04-17 11:05:08',NULL,0,NULL,1),(2,'微信用户信息同步任务','DefaultJob',0,0,'0 0 0/1 * * ? ','MsSystem.Schedule.Job','WxUserInfoJob',NULL,'2019-04-17 11:05:13',NULL,0,'2019-04-17 11:05:08',NULL,0,NULL,1);

/*Table structure for table `sys_button` */

DROP TABLE IF EXISTS `sys_button`;

CREATE TABLE `sys_button` (
  `ButtonId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ButtonName` varchar(50) NOT NULL COMMENT '菜单名称',
  `Memo` varchar(200) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`ButtonId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

/*Data for the table `sys_button` */

insert  into `sys_button`(`ButtonId`,`ButtonName`,`Memo`) values (1,'查看','查看'),(2,'新增','新增'),(3,'编辑','编辑'),(4,'删除','删除'),(5,'打印','打印'),(6,'审核','审核'),(7,'作废','作废'),(8,'结束','结束'),(9,'扩展','扩展');

/*Table structure for table `sys_data_privileges` */

DROP TABLE IF EXISTS `sys_data_privileges`;

CREATE TABLE `sys_data_privileges` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `DeptId` bigint(20) NOT NULL COMMENT '部门ID',
  `SystemId` bigint(20) NOT NULL COMMENT '系统ID',
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `DeptId` (`DeptId`),
  KEY `SystemId` (`SystemId`),
  CONSTRAINT `sys_data_privileges_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`UserId`),
  CONSTRAINT `sys_data_privileges_ibfk_2` FOREIGN KEY (`DeptId`) REFERENCES `sys_dept` (`DeptId`),
  CONSTRAINT `sys_data_privileges_ibfk_3` FOREIGN KEY (`SystemId`) REFERENCES `sys_system` (`SystemId`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 COMMENT='数据权限';

/*Data for the table `sys_data_privileges` */

insert  into `sys_data_privileges`(`Id`,`UserId`,`DeptId`,`SystemId`) values (8,1,1,1),(9,1,2,1),(10,1,3,1),(11,1,4,1);

/*Table structure for table `sys_dept` */

DROP TABLE IF EXISTS `sys_dept`;

CREATE TABLE `sys_dept` (
  `DeptId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '部门ID',
  `DeptName` varchar(50) NOT NULL COMMENT '部门名称',
  `DeptCode` varchar(50) DEFAULT NULL COMMENT '部门编号',
  `ParentId` bigint(20) NOT NULL COMMENT '父级ID',
  `Path` varchar(200) DEFAULT NULL COMMENT '路径',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除',
  `Memo` varchar(50) DEFAULT NULL COMMENT '备注',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人id',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`DeptId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='部门表（作用于全部系统）';

/*Data for the table `sys_dept` */

insert  into `sys_dept`(`DeptId`,`DeptName`,`DeptCode`,`ParentId`,`Path`,`IsDel`,`Memo`,`CreateUserId`,`CreateTime`) values (1,'MS软件','001',0,'1',0,'MS软件',1,1517812123),(2,'总经室','002',1,'1:2',0,'总经室',1,1517812220),(3,'研发部','003',1,'1:3',0,'研发部',1,1517814189),(4,'销售部','004',1,'1:4',0,'销售部',1,1517814213);

/*Table structure for table `sys_dept_leader` */

DROP TABLE IF EXISTS `sys_dept_leader`;

CREATE TABLE `sys_dept_leader` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `DeptId` bigint(20) NOT NULL COMMENT '部门ID',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `LeaderType` varchar(50) NOT NULL COMMENT '领导类型',
  PRIMARY KEY (`Id`),
  KEY `DeptId` (`DeptId`),
  KEY `UserId` (`UserId`),
  KEY `LeaderType` (`LeaderType`),
  CONSTRAINT `sys_dept_leader_ibfk_1` FOREIGN KEY (`DeptId`) REFERENCES `sys_dept` (`DeptId`),
  CONSTRAINT `sys_dept_leader_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`UserId`),
  CONSTRAINT `sys_dept_leader_ibfk_3` FOREIGN KEY (`LeaderType`) REFERENCES `sys_leader` (`Shorter`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='部门领导关系表';

/*Data for the table `sys_dept_leader` */

insert  into `sys_dept_leader`(`Id`,`DeptId`,`UserId`,`LeaderType`) values (1,3,1,'bmfzr');

/*Table structure for table `sys_leader` */

DROP TABLE IF EXISTS `sys_leader`;

CREATE TABLE `sys_leader` (
  `Shorter` varchar(50) NOT NULL COMMENT '缩写，公司领导应根据此字段获取',
  `LeaderName` varchar(50) NOT NULL COMMENT '级别名称',
  `Remark` varchar(200) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Shorter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='公司领导类型';

/*Data for the table `sys_leader` */

insert  into `sys_leader`(`Shorter`,`LeaderName`,`Remark`) values ('bmfjl','部门副经理','部门副经理'),('bmfzr','部门负责人','部门负责人/部门经理'),('boss','董事长','董事长/公司老板');

/*Table structure for table `sys_release_log` */

DROP TABLE IF EXISTS `sys_release_log`;

CREATE TABLE `sys_release_log` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `VersionNumber` varchar(50) NOT NULL COMMENT '版本号',
  `Memo` varchar(500) DEFAULT NULL COMMENT '描述',
  `CreateTime` bigint(20) NOT NULL COMMENT '发布时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;

/*Data for the table `sys_release_log` */

insert  into `sys_release_log`(`Id`,`VersionNumber`,`Memo`,`CreateTime`) values (1,'1','123',1498481230),(2,'2','123',1498481230),(3,'3','123',1498481230),(4,'4','123',1498481230),(5,'5','123',1498481230),(6,'6','123',1498481230),(7,'7','123',1498481230),(8,'8','123',1498481230),(9,'9','123',1498481230),(10,'10','123',1498481230),(11,'11','123',1498481230),(12,'12','123',1498481230),(13,'13','123',1498481230),(14,'14','123',1498481230),(15,'15','123',1498481230),(16,'16','123',1498481230),(17,'17','123',1498481230),(18,'18','123',1498481230),(19,'19','123',1498481230),(20,'20','123',1498481230),(21,'21','123',1498481230),(22,'22','123',1498481230),(23,'23','123',1498481230),(24,'24','123',1498481230),(25,'25','123',1498481230),(26,'26','123',1498481230);

/*Table structure for table `sys_resource` */

DROP TABLE IF EXISTS `sys_resource`;

CREATE TABLE `sys_resource` (
  `ResourceId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '资源ID',
  `SystemId` bigint(20) NOT NULL COMMENT '所属系统',
  `ResourceName` varchar(50) NOT NULL COMMENT '资源名称',
  `ParentId` bigint(11) NOT NULL DEFAULT '0' COMMENT '父级ID',
  `ResourceUrl` varchar(200) DEFAULT NULL COMMENT '资源地址',
  `Sort` int(11) NOT NULL COMMENT '同级排序',
  `ButtonClass` varchar(50) DEFAULT NULL COMMENT '按钮样式',
  `Icon` varchar(50) DEFAULT NULL COMMENT '样式图标ICON',
  `IsShow` tinyint(4) NOT NULL COMMENT '是否显示到菜单',
  `CreateUserId` bigint(11) NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  `Memo` varchar(200) DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除',
  `IsButton` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否是按钮',
  `ButtonType` tinyint(4) DEFAULT NULL COMMENT '按钮类型',
  `Path` varchar(50) DEFAULT NULL COMMENT '资源路径',
  PRIMARY KEY (`ResourceId`),
  KEY `ParentId` (`ParentId`),
  KEY `SystemId` (`SystemId`),
  CONSTRAINT `sys_resource_ibfk_1` FOREIGN KEY (`SystemId`) REFERENCES `sys_system` (`SystemId`)
) ENGINE=InnoDB AUTO_INCREMENT=275 DEFAULT CHARSET=utf8 COMMENT='资源表';

/*Data for the table `sys_resource` */

insert  into `sys_resource`(`ResourceId`,`SystemId`,`ResourceName`,`ParentId`,`ResourceUrl`,`Sort`,`ButtonClass`,`Icon`,`IsShow`,`CreateUserId`,`CreateTime`,`Memo`,`IsDel`,`IsButton`,`ButtonType`,`Path`) values (1,1,'系统管理',0,'/',1,NULL,'fa fa-cogs',1,1,1,'123',0,0,NULL,'1'),(2,1,'菜单管理',1,'/Sys/Resource/Index',5,NULL,'fa fa-bars',1,1,1,NULL,0,0,NULL,'1:2'),(4,1,'菜单管理编辑页',2,'/Sys/Resource/Show',0,NULL,'fa fa-balance-scale',0,1,1,NULL,0,0,NULL,'2:4'),(48,1,'查看',4,NULL,4,NULL,'fa fa-search',0,0,1503957966,NULL,0,1,1,NULL),(49,1,'新增',4,NULL,1,NULL,'fa fa-plus',0,0,1503957966,NULL,0,1,2,NULL),(50,1,'编辑',4,NULL,2,NULL,'fa fa-edit',0,0,1503957966,NULL,0,1,3,NULL),(51,1,'删除',4,NULL,3,NULL,'fa fa-trash',0,0,1503957966,NULL,0,1,4,NULL),(53,1,'角色管理',1,'/Sys/Role/Index',2,NULL,'fa fa-group',1,1,1503958727,'123',0,0,NULL,'1:53'),(54,1,'查看',53,NULL,4,NULL,'fa fa-search',0,0,1503958727,NULL,0,1,1,NULL),(55,1,'新增',53,NULL,1,NULL,'fa fa-plus',0,0,1503958727,NULL,0,1,2,NULL),(56,1,'编辑',53,NULL,2,NULL,'fa fa-edit',0,0,1503958727,NULL,0,1,3,NULL),(57,1,'删除',53,NULL,3,NULL,'fa fa-trash',0,0,1503958727,NULL,0,1,4,NULL),(58,1,'子系统管理',1,'/Sys/System/Index',4,NULL,'fa fa-cog',1,1,1503958798,'123',0,0,NULL,'1:58'),(59,1,'查看',58,NULL,4,NULL,'fa fa-search',0,0,1503958825,NULL,0,1,1,NULL),(60,1,'新增',58,NULL,1,NULL,'fa fa-plus',0,0,1503958825,NULL,0,1,2,NULL),(61,1,'编辑',58,NULL,2,NULL,'fa fa-edit',0,0,1503958825,NULL,0,1,3,NULL),(62,1,'删除',58,NULL,3,NULL,'fa fa-trash',0,0,1503958825,NULL,0,1,4,NULL),(64,1,'编辑',2,NULL,2,NULL,'fa fa-edit',0,0,1503958921,NULL,0,1,3,NULL),(65,1,'删除',2,NULL,3,NULL,'fa fa-trash',0,0,1503958921,NULL,0,1,4,NULL),(66,1,'角色编辑页',53,'/Sys/Role/Show',0,NULL,NULL,0,1,1503959673,NULL,0,0,NULL,'53:66'),(67,1,'查看',66,NULL,4,NULL,'fa fa-search',0,0,1503959674,NULL,0,1,1,NULL),(68,1,'新增',66,NULL,1,NULL,'fa fa-plus',0,0,1503959674,NULL,0,1,2,NULL),(69,1,'编辑',66,NULL,2,NULL,'fa fa-edit',0,0,1503959674,NULL,0,1,3,NULL),(70,1,'删除',66,NULL,3,NULL,'fa fa-trash',0,0,1503959674,NULL,0,1,4,NULL),(71,1,'系统编辑页',58,'/Sys/System/Show',0,NULL,NULL,0,1,1503959721,NULL,0,0,NULL,'58:71'),(72,1,'查看',71,NULL,4,NULL,'fa fa-search',0,0,1503959721,NULL,0,1,1,NULL),(73,1,'新增',71,NULL,1,NULL,'fa fa-plus',0,0,1503959721,NULL,0,1,2,NULL),(74,1,'编辑',71,NULL,2,NULL,'fa fa-edit',0,0,1503959721,NULL,0,1,3,NULL),(75,1,'删除',71,NULL,3,NULL,'fa fa-trash',0,0,1503959721,NULL,0,1,4,NULL),(76,1,'用户管理',1,'/Sys/User/Index',1,NULL,'fa fa-user',1,1,1503959782,NULL,0,0,NULL,'1:76'),(77,1,'查看',76,NULL,4,NULL,'fa fa-search',0,0,1503959782,NULL,0,1,1,NULL),(78,1,'新增',76,NULL,1,NULL,'fa fa-plus',0,0,1503959782,NULL,0,1,2,NULL),(79,1,'编辑',76,NULL,2,NULL,'fa fa-edit',0,0,1503959782,NULL,0,1,3,NULL),(80,1,'删除',76,NULL,3,NULL,'fa fa-trash',0,0,1503959782,NULL,0,1,4,NULL),(81,1,'用户编辑页',76,'/Sys/User/Show',0,NULL,NULL,0,1,1503959818,NULL,0,0,NULL,'76:81'),(82,1,'查看',81,NULL,4,NULL,'fa fa-search',0,0,1503959818,NULL,0,1,1,NULL),(83,1,'新增',81,NULL,1,NULL,'fa fa-plus',0,0,1503959818,NULL,0,1,2,NULL),(84,1,'编辑',81,NULL,2,NULL,'fa fa-edit',0,0,1503959818,NULL,0,1,3,NULL),(85,1,'删除',81,NULL,3,NULL,'fa fa-trash',0,0,1503959818,NULL,0,1,4,NULL),(86,1,'查看',2,NULL,4,NULL,'fa fa-search',0,0,1503990906,NULL,0,1,1,NULL),(87,1,'新增',2,NULL,1,NULL,'fa fa-plus',0,0,1503990906,NULL,0,1,2,NULL),(88,2,'工作流',0,'/',3,NULL,'fa fa-sitemap',1,1,1504013557,NULL,0,0,NULL,'88'),(91,2,'流程设计',88,'/WF/WorkFlow/Index',3,NULL,'fa fa-legal',1,1,1504439709,'流程设计列表',0,0,NULL,'88:91'),(92,2,'查看',91,NULL,4,NULL,'fa fa-search',0,0,1504439709,NULL,0,1,1,NULL),(93,2,'我的待办',88,'/WF/WorkFlowInstance/TodoList',1,NULL,'fa fa-user',1,1,1504439745,'我的待办',0,0,NULL,'88:93'),(94,2,'审批历史',88,'/WF/WorkFlowInstance/MyApprovalHistory',0,NULL,'fa fa-history',1,1,1504575850,'审批历史记录',0,0,NULL,'88:94'),(95,2,'查看',93,NULL,4,NULL,'fa fa-search',0,0,1504575862,NULL,0,1,1,NULL),(96,2,'查看',94,NULL,4,NULL,'fa fa-search',0,0,1504575866,NULL,0,1,1,NULL),(97,3,'微信管理',0,'/',2,NULL,'fa fa-weixin',1,1,1504576048,'微信管理',0,0,NULL,'97'),(98,2,'流程分类',88,'/WF/Category/Index',4,NULL,'fa fa-building-o',1,1,1508764750,'流程分类',0,0,NULL,'88:98'),(99,2,'查看',98,NULL,4,NULL,'fa fa-search',0,0,1508764750,NULL,0,1,1,NULL),(100,5,'行政办公',0,'/',2,NULL,'fa fa-book',1,1,1509884326,NULL,0,0,NULL,'100'),(142,1,'部门管理',1,'/Sys/Dept/Index',3,NULL,'fa fa-sitemap',1,1,1514899755,NULL,0,0,NULL,'1:142'),(143,1,'查看',142,NULL,4,NULL,'fa fa-search',0,0,1514899755,NULL,0,1,1,NULL),(144,1,'新增',142,NULL,1,NULL,'fa fa-plus',0,0,1514899755,NULL,0,1,2,NULL),(145,1,'编辑',142,NULL,2,NULL,'fa fa-edit',0,0,1514899755,NULL,0,1,3,NULL),(146,1,'删除',142,NULL,3,NULL,'fa fa-trash',0,0,1514899755,NULL,0,1,4,NULL),(152,1,'部门编辑页',142,'/Sys/Dept/Show',1,NULL,'',0,1,1514899820,NULL,0,0,NULL,'1:142:152'),(153,1,'查看',152,NULL,4,NULL,'fa fa-search',0,0,1514899820,NULL,0,1,1,NULL),(154,1,'新增',152,NULL,1,NULL,'fa fa-plus',0,0,1514899820,NULL,0,1,2,NULL),(155,1,'编辑',152,NULL,2,NULL,'fa fa-edit',0,0,1514899820,NULL,0,1,3,NULL),(156,1,'删除',152,NULL,3,NULL,'fa fa-trash',0,0,1514899820,NULL,0,1,4,NULL),(157,1,'数据权限',76,'/Sys/User/DataPrivileges',2,NULL,NULL,0,1,1515548644,NULL,0,0,NULL,'1:76:157'),(158,1,'查看',157,NULL,4,NULL,'fa fa-search',0,0,1515548644,NULL,0,1,1,NULL),(159,1,'新增',157,NULL,1,NULL,'fa fa-plus',0,0,1515548644,NULL,0,1,2,NULL),(160,1,'编辑',157,NULL,2,NULL,'fa fa-edit',0,0,1515548644,NULL,0,1,3,NULL),(161,1,'删除',157,NULL,3,NULL,'fa fa-trash',0,0,1515548644,NULL,0,1,4,NULL),(162,5,'员工请假',100,'/OA/Leave/Index',1,NULL,'fa fa-hand-paper-o',1,1,1519985181,NULL,0,0,NULL,'100:162'),(163,5,'查看',162,NULL,0,NULL,'fa fa-search',0,0,1519985181,NULL,0,1,1,NULL),(164,5,'新增',162,NULL,0,NULL,'fa fa-plus',0,0,1519985181,NULL,0,1,2,NULL),(165,5,'编辑',162,NULL,0,NULL,'fa fa-edit',0,0,1519985181,NULL,0,1,3,NULL),(166,5,'删除',162,NULL,0,NULL,'fa fa-trash',0,0,1519985181,NULL,0,1,4,NULL),(167,5,'员工请假编辑页',162,'/OA/Leave/Show',1,NULL,'fa fa-cc-diners-club',0,1,1520040301,NULL,0,0,NULL,'162:167'),(168,5,'查看',167,NULL,0,NULL,'fa fa-search',0,0,1520040301,NULL,0,1,1,NULL),(169,5,'新增',167,NULL,0,NULL,'fa fa-plus',0,0,1520040301,NULL,0,1,2,NULL),(170,5,'编辑',167,NULL,0,NULL,'fa fa-edit',0,0,1520040301,NULL,0,1,3,NULL),(171,5,'删除',167,NULL,0,NULL,'fa fa-trash',0,0,1520040301,NULL,0,1,4,NULL),(172,2,'新增',91,NULL,0,NULL,'fa fa-plus',0,0,1544438530,NULL,0,1,2,NULL),(173,2,'编辑',91,NULL,0,NULL,'fa fa-edit',0,0,1544438530,NULL,0,1,3,NULL),(174,2,'删除',91,NULL,0,NULL,'fa fa-trash',0,0,1544438530,NULL,0,1,4,NULL),(175,1,'调度中心',1,'/Schedule/Home/Index',6,NULL,'fa fa-joomla',1,1,1545365625,NULL,0,0,NULL,'1:175'),(176,1,'查看',175,NULL,0,NULL,'fa fa-search',0,0,1545365625,NULL,0,1,1,NULL),(177,1,'新增',175,NULL,0,NULL,'fa fa-plus',0,0,1545365625,NULL,0,1,2,NULL),(178,1,'编辑',175,NULL,0,NULL,'fa fa-edit',0,0,1545365625,NULL,0,1,3,NULL),(179,1,'删除',175,NULL,0,NULL,'fa fa-trash',0,0,1545365625,NULL,0,1,4,NULL),(180,1,'调度编辑页',175,'/Schedule/Home/Show',1,NULL,NULL,0,1,1545365682,NULL,0,0,NULL,'1:175:180'),(181,1,'查看',180,NULL,0,NULL,'fa fa-search',0,0,1545365682,NULL,0,1,1,NULL),(182,1,'新增',180,NULL,0,NULL,'fa fa-plus',0,0,1545365754,NULL,0,1,2,NULL),(183,1,'编辑',180,NULL,0,NULL,'fa fa-edit',0,0,1545365754,NULL,0,1,3,NULL),(184,1,'删除',180,NULL,0,NULL,'fa fa-trash',0,0,1545365754,NULL,0,1,4,NULL),(185,1,'日志列表',1,'/Sys/Log/Index',7,NULL,'fa fa-list-alt',1,1,1545902555,NULL,0,0,NULL,'1:185'),(186,1,'查看',185,NULL,0,NULL,'fa fa-search',0,0,1545902555,NULL,0,1,1,NULL),(187,3,'规则管理',97,'/Weixin/Rule/Index',3,NULL,'fa fa-hand-lizard-o',1,1,1547040097,NULL,0,0,NULL,'97:187'),(188,3,'查看',187,NULL,0,NULL,'fa fa-search',0,0,1547040097,NULL,0,1,1,NULL),(189,3,'新增',187,NULL,0,NULL,'fa fa-plus',0,0,1547040097,NULL,0,1,2,NULL),(190,3,'编辑',187,NULL,0,NULL,'fa fa-edit',0,0,1547040097,NULL,0,1,3,NULL),(191,3,'删除',187,NULL,0,NULL,'fa fa-trash',0,0,1547040097,NULL,0,1,4,NULL),(192,3,'规则编辑页',187,'/Weixin/Rule/Show',1,NULL,NULL,0,1,1547040143,NULL,0,0,NULL,'97:187:192'),(193,3,'查看',192,NULL,0,NULL,'fa fa-search',0,0,1547040143,NULL,0,1,1,NULL),(194,3,'新增',192,NULL,0,NULL,'fa fa-plus',0,0,1547040143,NULL,0,1,2,NULL),(195,3,'编辑',192,NULL,0,NULL,'fa fa-edit',0,0,1547040143,NULL,0,1,3,NULL),(196,3,'删除',192,NULL,0,NULL,'fa fa-trash',0,0,1547040143,NULL,0,1,4,NULL),(197,3,'自定义菜单',97,'/Weixin/Menu/Index',2,NULL,'fa fa-bars',1,1,1547040193,NULL,0,0,NULL,'97:197'),(198,3,'查看',197,NULL,0,NULL,'fa fa-search',0,0,1547040193,NULL,0,1,1,NULL),(199,3,'新增',197,NULL,0,NULL,'fa fa-plus',0,0,1547040193,NULL,0,1,2,NULL),(200,3,'编辑',197,NULL,0,NULL,'fa fa-edit',0,0,1547040193,NULL,0,1,3,NULL),(201,3,'删除',197,NULL,0,NULL,'fa fa-trash',0,0,1547040193,NULL,0,1,4,NULL),(202,3,'自定义菜单编辑页',197,'/Weixin/Menu/Show',1,NULL,NULL,0,1,1547040223,NULL,0,0,NULL,'97:197:202'),(203,3,'查看',202,NULL,0,NULL,'fa fa-search',0,0,1547040223,NULL,0,1,1,NULL),(204,3,'新增',202,NULL,0,NULL,'fa fa-plus',0,0,1547040223,NULL,0,1,2,NULL),(205,3,'编辑',202,NULL,0,NULL,'fa fa-edit',0,0,1547040223,NULL,0,1,3,NULL),(206,3,'删除',202,NULL,0,NULL,'fa fa-trash',0,0,1547040223,NULL,0,1,4,NULL),(223,2,'新增',98,NULL,0,'fa fa-plus',NULL,0,0,1556070241,NULL,0,1,2,NULL),(224,2,'编辑',98,NULL,0,'fa fa-edit',NULL,0,0,1556070241,NULL,0,1,3,NULL),(225,2,'删除',98,NULL,0,'fa fa-trash',NULL,0,0,1556070241,NULL,0,1,4,NULL),(226,2,'我的流程',88,'/WF/WorkFlowInstance/MyFlow',2,NULL,'fa fa-user-plus',1,1,1556096263,NULL,0,0,NULL,'88:226'),(227,2,'查看',226,NULL,0,'fa fa-search',NULL,0,0,1556096263,NULL,0,1,1,NULL),(228,2,'新增',226,NULL,0,'fa fa-plus',NULL,0,0,1556096263,NULL,0,1,2,NULL),(229,2,'编辑',226,NULL,0,'fa fa-edit',NULL,0,0,1556096263,NULL,0,1,3,NULL),(230,2,'删除',226,NULL,0,'fa fa-trash',NULL,0,0,1556096263,NULL,0,1,4,NULL),(231,2,'流程发起',88,'/WF/WorkFlowInstance/Start',5,NULL,'fa fa-location-arrow',1,1,1556096629,NULL,0,0,NULL,'88:231'),(232,2,'查看',231,NULL,0,'fa fa-search',NULL,0,0,1556096629,NULL,0,1,1,NULL),(236,2,'表单设计',88,'/WF/Form/Index',6,NULL,'fa fa-contao',1,1,1556097850,'表单设计',0,0,NULL,'88:236'),(237,2,'查看',236,NULL,0,'fa fa-search',NULL,0,0,1556097850,NULL,0,1,1,NULL),(238,2,'新增',236,NULL,0,'fa fa-plus',NULL,0,0,1556097850,NULL,0,1,2,NULL),(239,2,'编辑',236,NULL,0,'fa fa-edit',NULL,0,0,1556097850,NULL,0,1,3,NULL),(240,2,'删除',236,NULL,0,'fa fa-trash',NULL,0,0,1556097850,NULL,0,1,4,NULL),(241,2,'表单设计编辑页',236,'/WF/Form/Show',1,NULL,NULL,0,1,1556098005,NULL,0,0,NULL,'88:236:241'),(242,2,'查看',241,NULL,0,'fa fa-search',NULL,0,0,1556098005,NULL,0,1,1,NULL),(243,2,'新增',241,NULL,0,'fa fa-plus',NULL,0,0,1556098005,NULL,0,1,2,NULL),(244,2,'编辑',241,NULL,0,'fa fa-edit',NULL,0,0,1556098005,NULL,0,1,3,NULL),(245,2,'删除',241,NULL,0,'fa fa-trash',NULL,0,0,1556098005,NULL,0,1,4,NULL),(246,2,'流程设计编辑页',91,'/WF/WorkFlow/Show',1,NULL,NULL,0,1,1557981722,'流程设计编辑页',0,0,NULL,'88:91:246'),(247,2,'查看',246,NULL,0,'fa fa-search',NULL,0,0,1557981722,NULL,0,1,1,NULL),(248,2,'新增',246,NULL,0,'fa fa-plus',NULL,0,0,1557981722,NULL,0,1,2,NULL),(249,2,'编辑',246,NULL,0,'fa fa-edit',NULL,0,0,1557981722,NULL,0,1,3,NULL),(250,2,'删除',246,NULL,0,'fa fa-trash',NULL,0,0,1557981722,NULL,0,1,4,NULL),(253,5,'消息管理',100,'/OA/Message/Index',2,NULL,'fa fa-envelope-o',1,1,1558927920,NULL,0,0,NULL,'100:253'),(254,5,'查看',253,NULL,0,'fa fa-search',NULL,0,0,1558927920,NULL,0,1,1,NULL),(255,5,'新增',253,NULL,0,'fa fa-plus',NULL,0,0,1558927920,NULL,0,1,2,NULL),(256,5,'编辑',253,NULL,0,'fa fa-edit',NULL,0,0,1558927920,NULL,0,1,3,NULL),(257,5,'删除',253,NULL,0,'fa fa-trash',NULL,0,0,1558927920,NULL,0,1,4,NULL),(258,5,'立即发送',253,NULL,0,'fa fa-location-arrow',NULL,0,0,1558927931,NULL,0,1,9,NULL),(259,5,'消息编辑页',253,'/OA/Message/Show',1,NULL,NULL,0,1,1558927982,NULL,0,0,NULL,'100:253:259'),(260,5,'查看',259,NULL,0,'fa fa-search',NULL,0,0,1558927982,NULL,0,1,1,NULL),(261,5,'新增',259,NULL,0,'fa fa-plus',NULL,0,0,1558927982,NULL,0,1,2,NULL),(262,5,'编辑',259,NULL,0,'fa fa-edit',NULL,0,0,1558927982,NULL,0,1,3,NULL),(263,5,'删除',259,NULL,0,'fa fa-trash',NULL,0,0,1558927982,NULL,0,1,4,NULL),(264,5,'我的消息',100,'/OA/Message/MyList',3,NULL,'fa fa-commenting-o',1,1,1558928061,NULL,0,0,NULL,'100:264'),(265,5,'查看',264,NULL,0,'fa fa-search',NULL,0,0,1558928061,NULL,0,1,1,NULL),(266,5,'消息明细',264,'/OA/Message/Detail',1,NULL,NULL,0,1,1558946268,NULL,0,0,NULL,'100:264:266'),(267,5,'查看',266,NULL,0,'fa fa-search',NULL,0,0,1558946268,NULL,0,1,1,NULL),(268,3,'公众号',97,'/Weixin/Account/Index',1,NULL,'fa fa-user',1,1,1560503093,'公众号列表',0,0,NULL,'97:268'),(269,3,'查看',268,NULL,0,'fa fa-search',NULL,0,0,1560503093,NULL,0,1,1,NULL),(270,3,'新增',268,NULL,0,'fa fa-plus',NULL,0,0,1560503093,NULL,0,1,2,NULL),(271,3,'编辑',268,NULL,0,'fa fa-edit',NULL,0,0,1560503093,NULL,0,1,3,NULL),(272,3,'删除',268,NULL,0,'fa fa-trash',NULL,0,0,1560503093,NULL,0,1,4,NULL),(273,5,'内部聊天',100,'/OA/Chat/Index',4,NULL,'fa fa-whatsapp',1,1,1560740703,'即时通信聊天',0,0,NULL,'100:273'),(274,5,'查看',273,NULL,0,'fa fa-search',NULL,0,0,1560740703,NULL,0,1,1,NULL);

/*Table structure for table `sys_role` */

DROP TABLE IF EXISTS `sys_role`;

CREATE TABLE `sys_role` (
  `RoleId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '角色ID',
  `SystemId` bigint(20) NOT NULL COMMENT '所属系统',
  `RoleName` varchar(50) NOT NULL COMMENT '角色名称',
  `Memo` varchar(200) DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `UpdateUserId` bigint(20) DEFAULT NULL COMMENT '修改人',
  `UpdateTime` bigint(20) DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`RoleId`),
  KEY `SystemId` (`SystemId`),
  CONSTRAINT `sys_role_ibfk_1` FOREIGN KEY (`SystemId`) REFERENCES `sys_system` (`SystemId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='角色表';

/*Data for the table `sys_role` */

insert  into `sys_role`(`RoleId`,`SystemId`,`RoleName`,`Memo`,`IsDel`,`CreateUserId`,`CreateTime`,`UpdateUserId`,`UpdateTime`) values (1,1,'系统管理员','系统管理员',0,0,1497889200,NULL,NULL),(2,1,'微信管理员','微信管理员',1,0,1497889200,0,0),(4,1,'测试角色','测试角色',1,1,1499488686,NULL,NULL),(5,2,'流程管理员','流程管理员',0,1,1499493280,NULL,NULL),(6,5,'普通用户','普通用户123',0,1,1500956464,NULL,NULL),(7,3,'微信管理员','微信管理员',0,1,1547040281,0,0);

/*Table structure for table `sys_role_resource` */

DROP TABLE IF EXISTS `sys_role_resource`;

CREATE TABLE `sys_role_resource` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RoleId` bigint(20) NOT NULL COMMENT '角色ID',
  `ResourceId` bigint(20) NOT NULL COMMENT '资源ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`Id`),
  KEY `RoleId` (`RoleId`),
  KEY `ResourceId` (`ResourceId`),
  CONSTRAINT `sys_role_resource_ibfk_1` FOREIGN KEY (`RoleId`) REFERENCES `sys_role` (`RoleId`),
  CONSTRAINT `sys_role_resource_ibfk_2` FOREIGN KEY (`ResourceId`) REFERENCES `sys_resource` (`ResourceId`)
) ENGINE=InnoDB AUTO_INCREMENT=2301 DEFAULT CHARSET=utf8 COMMENT='角色资源关联表';

/*Data for the table `sys_role_resource` */

insert  into `sys_role_resource`(`Id`,`RoleId`,`ResourceId`,`CreateTime`) values (2102,1,1,1558946119),(2103,1,76,1558946119),(2104,1,81,1558946119),(2105,1,83,1558946119),(2106,1,84,1558946119),(2107,1,85,1558946119),(2108,1,82,1558946119),(2109,1,78,1558946119),(2110,1,79,1558946119),(2111,1,157,1558946119),(2112,1,159,1558946119),(2113,1,160,1558946119),(2114,1,161,1558946119),(2115,1,158,1558946119),(2116,1,80,1558946119),(2117,1,77,1558946119),(2118,1,53,1558946119),(2119,1,66,1558946119),(2120,1,68,1558946119),(2121,1,69,1558946119),(2122,1,70,1558946119),(2123,1,67,1558946119),(2124,1,55,1558946119),(2125,1,56,1558946119),(2126,1,57,1558946119),(2127,1,54,1558946119),(2128,1,142,1558946119),(2129,1,144,1558946119),(2130,1,152,1558946119),(2131,1,154,1558946119),(2132,1,155,1558946119),(2133,1,156,1558946119),(2134,1,153,1558946119),(2135,1,145,1558946119),(2136,1,146,1558946119),(2137,1,143,1558946119),(2138,1,58,1558946119),(2139,1,71,1558946119),(2140,1,73,1558946119),(2141,1,74,1558946119),(2142,1,75,1558946119),(2143,1,72,1558946119),(2144,1,60,1558946119),(2145,1,61,1558946119),(2146,1,62,1558946119),(2147,1,59,1558946119),(2148,1,2,1558946119),(2149,1,4,1558946119),(2150,1,49,1558946119),(2151,1,50,1558946119),(2152,1,51,1558946119),(2153,1,48,1558946119),(2154,1,87,1558946119),(2155,1,64,1558946119),(2156,1,65,1558946119),(2157,1,86,1558946119),(2158,1,185,1558946119),(2159,1,186,1558946119),(2210,5,88,1559053008),(2211,5,94,1559053008),(2212,5,96,1559053008),(2213,5,93,1559053008),(2214,5,95,1559053008),(2215,5,226,1559053008),(2216,5,227,1559053008),(2217,5,228,1559053008),(2218,5,229,1559053008),(2219,5,230,1559053008),(2220,5,91,1559053008),(2221,5,172,1559053008),(2222,5,173,1559053008),(2223,5,174,1559053008),(2224,5,246,1559053008),(2225,5,247,1559053008),(2226,5,248,1559053008),(2227,5,249,1559053008),(2228,5,250,1559053008),(2229,5,92,1559053008),(2230,5,98,1559053008),(2231,5,223,1559053008),(2232,5,224,1559053008),(2233,5,225,1559053008),(2234,5,99,1559053008),(2235,5,231,1559053008),(2236,5,232,1559053008),(2237,5,236,1559053008),(2238,5,237,1559053008),(2239,5,238,1559053008),(2240,5,239,1559053008),(2241,5,240,1559053008),(2242,5,241,1559053008),(2243,5,242,1559053008),(2244,5,243,1559053008),(2245,5,244,1559053008),(2246,5,245,1559053008),(2247,7,97,1560503131),(2248,7,268,1560503131),(2249,7,269,1560503131),(2250,7,270,1560503131),(2251,7,271,1560503131),(2252,7,272,1560503131),(2253,7,197,1560503131),(2254,7,198,1560503131),(2255,7,199,1560503131),(2256,7,200,1560503131),(2257,7,201,1560503131),(2258,7,202,1560503131),(2259,7,203,1560503131),(2260,7,204,1560503131),(2261,7,205,1560503131),(2262,7,206,1560503131),(2263,7,187,1560503131),(2264,7,188,1560503131),(2265,7,189,1560503131),(2266,7,190,1560503131),(2267,7,191,1560503131),(2268,7,192,1560503131),(2269,7,193,1560503131),(2270,7,194,1560503131),(2271,7,195,1560503131),(2272,7,196,1560503131),(2273,6,100,1560740715),(2274,6,162,1560740715),(2275,6,163,1560740715),(2276,6,164,1560740715),(2277,6,165,1560740715),(2278,6,166,1560740715),(2279,6,167,1560740715),(2280,6,168,1560740715),(2281,6,169,1560740715),(2282,6,170,1560740715),(2283,6,171,1560740715),(2284,6,253,1560740715),(2285,6,254,1560740715),(2286,6,255,1560740715),(2287,6,256,1560740715),(2288,6,257,1560740715),(2289,6,258,1560740715),(2290,6,259,1560740715),(2291,6,260,1560740715),(2292,6,261,1560740715),(2293,6,262,1560740715),(2294,6,263,1560740715),(2295,6,264,1560740715),(2296,6,265,1560740715),(2297,6,266,1560740715),(2298,6,267,1560740715),(2299,6,273,1560740715),(2300,6,274,1560740715);

/*Table structure for table `sys_system` */

DROP TABLE IF EXISTS `sys_system`;

CREATE TABLE `sys_system` (
  `SystemId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '系统ID',
  `SystemName` varchar(50) NOT NULL COMMENT '系统名称',
  `SystemCode` varchar(50) NOT NULL COMMENT '系统编码',
  `Memo` varchar(100) DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) DEFAULT NULL COMMENT '创建时间',
  `UpdateUserId` bigint(20) DEFAULT NULL COMMENT '更新人',
  `UpdateTime` bigint(20) DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`SystemId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COMMENT='系统表';

/*Data for the table `sys_system` */

insert  into `sys_system`(`SystemId`,`SystemName`,`SystemCode`,`Memo`,`IsDel`,`Sort`,`CreateUserId`,`CreateTime`,`UpdateUserId`,`UpdateTime`) values (1,'系统管理','be1c63a0-63aa-11e7-a221-f97d872f551b','权限系统',0,1,1,1498481230,0,1537448282),(2,'工作流','c3cd9538-63aa-11e7-a221-f97d872f551b','工作流',0,4,1,1498481230,0,1537447644),(3,'微信系统','c7526ad9-63aa-11e7-a221-f97d872f551b','微信系统',0,3,1,1498481230,0,1537447638),(4,'系统测试','252110d2-cb56-46c8-9dc2-64c3d7e23b21','系统测试0000',1,0,1,1499494026,0,0),(5,'行政办公系统','d65fb3df-d342-41c9-ad9a-3faedbb5b0dc','行政办公系统',0,2,1,1500955747,0,1537448139),(6,'行政办公','8130e64e-912b-4ce5-aae5-89ba3b5d97a9','行政办公系统',1,0,1,1509884116,NULL,NULL);

/*Table structure for table `sys_user` */

DROP TABLE IF EXISTS `sys_user`;

CREATE TABLE `sys_user` (
  `UserId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Account` varchar(50) DEFAULT NULL COMMENT '登录账号',
  `UserName` varchar(50) NOT NULL COMMENT '用户名',
  `JobNumber` varchar(50) DEFAULT NULL COMMENT '工号',
  `Password` varchar(50) NOT NULL COMMENT '密码',
  `HeadImg` varchar(200) DEFAULT NULL COMMENT '头像地址',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除 1:是，0：否',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `UpdateUserId` bigint(20) DEFAULT NULL COMMENT '更新人',
  `UpdateTime` bigint(20) DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`UserId`),
  KEY `Account` (`Account`),
  KEY `JobNumber` (`JobNumber`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8 COMMENT='用户表（作用于全部系统）';

/*Data for the table `sys_user` */

insert  into `sys_user`(`UserId`,`Account`,`UserName`,`JobNumber`,`Password`,`HeadImg`,`IsDel`,`CreateUserId`,`CreateTime`,`UpdateUserId`,`UpdateTime`) values (1,'wms','wms','20180101','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',0,1,12,1,1542809506),(4,'wangwu','王五123','20180102','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',0,0,1498571322,1,1560497334),(5,'zhangsan','张三','20180103','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/1ca449c6-24ed-4b78-a032-6005990ff707.jpeg',0,0,1499750510,1,1538660578),(6,'lisi','李四aa','20180104','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',0,0,1499750523,NULL,NULL),(7,'123','123','20180105','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',0,0,1499750534,1,1557909906),(8,'321','321','20180106','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1499750544,NULL,NULL),(9,'1234','1234','20180107','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1499750555,NULL,NULL),(10,'1234','1234','20180108','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1499750555,NULL,NULL),(11,'asd','asd','20180109','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1499750583,NULL,NULL),(12,'asd','asd','20180110','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1499750584,NULL,NULL),(13,'aaa','aaa','20180111','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1499750592,NULL,NULL),(14,'aaa','aaa','20180112','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1499750592,NULL,NULL),(15,'bbb','bbb','20180113','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1501310757,NULL,NULL),(16,'ccc','ccc','20180114','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1501310765,NULL,NULL),(17,'ddd','ddd','20180115','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1501310778,NULL,NULL),(18,'eee','eee','20180116','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1501310789,NULL,NULL),(19,'asd','asd','20180117','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1509869141,NULL,NULL),(20,'123','123','2018102098','A93C168323147D1135503939396CAC628DC194C5','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1539993966,NULL,NULL),(21,'cs','cs','2019041302','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',1,0,1555123202,NULL,NULL);

/*Table structure for table `sys_user_dept` */

DROP TABLE IF EXISTS `sys_user_dept`;

CREATE TABLE `sys_user_dept` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `DeptId` bigint(20) NOT NULL COMMENT '部门ID',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`Id`),
  KEY `DeptId` (`DeptId`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `sys_user_dept_ibfk_1` FOREIGN KEY (`DeptId`) REFERENCES `sys_dept` (`DeptId`),
  CONSTRAINT `sys_user_dept_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Data for the table `sys_user_dept` */

insert  into `sys_user_dept`(`Id`,`DeptId`,`UserId`,`CreateTime`) values (4,4,6,1557303745),(5,3,1,1559469199),(6,3,4,1559792769);

/*Table structure for table `sys_user_role` */

DROP TABLE IF EXISTS `sys_user_role`;

CREATE TABLE `sys_user_role` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `RoleId` bigint(20) NOT NULL COMMENT '角色ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `RoleId` (`RoleId`),
  CONSTRAINT `sys_user_role_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`UserId`),
  CONSTRAINT `sys_user_role_ibfk_2` FOREIGN KEY (`RoleId`) REFERENCES `sys_role` (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8 COMMENT='用户角色关联表';

/*Data for the table `sys_user_role` */

insert  into `sys_user_role`(`Id`,`UserId`,`RoleId`,`CreateTime`) values (47,1,1,1547040313),(48,1,5,1547040313),(49,1,7,1547040313),(50,1,6,1547040313),(51,4,1,1557214422),(52,4,5,1557214422),(56,6,6,1557303730),(58,6,1,1557304953),(59,7,5,1557909952),(60,7,6,1557909952),(61,4,6,1559053056),(62,5,1,1559702623),(63,5,5,1559702623),(64,5,6,1559702623);

/*Table structure for table `sys_workflowsql` */

DROP TABLE IF EXISTS `sys_workflowsql`;

CREATE TABLE `sys_workflowsql` (
  `Name` varchar(50) NOT NULL COMMENT '流程sql名称,必须是以sys_为开头，用于判断属于哪个系统，方便调用接口',
  `FlowSQL` text NOT NULL COMMENT '流程SQL，执行结果必须是一行一列',
  `Param` varchar(200) DEFAULT NULL COMMENT '参数名。以英文 , 分割',
  `SQLType` tinyint(4) NOT NULL DEFAULT '0' COMMENT '类型，0：选人节点，必须返回的是用户ID，1：连线条件，条件通过返回的是一行一列的数据，不通过没有任何返回结果',
  `Status` int(11) NOT NULL DEFAULT '1' COMMENT '状态',
  `Remark` varchar(200) DEFAULT NULL COMMENT '备注',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  PRIMARY KEY (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用于工作流获取权限系统数据';

/*Data for the table `sys_workflowsql` */

insert  into `sys_workflowsql`(`Name`,`FlowSQL`,`Param`,`SQLType`,`Status`,`Remark`,`CreateTime`,`CreateUserId`) values ('sys_getdeptleader','SELECT DISTINCT dl.`UserId` FROM sys_user u \r\nLEFT JOIN sys_user_dept ud ON ud.`UserId`=u.`UserId`\r\nLEFT JOIN sys_dept_leader dl ON dl.`DeptId`=ud.`DeptId`\r\nWHERE u.`UserId`=@userid','userid',0,1,'权限系统，获取部门负责人',1,1);

/*Table structure for table `wf_workflow` */

DROP TABLE IF EXISTS `wf_workflow`;

CREATE TABLE `wf_workflow` (
  `FlowId` char(36) NOT NULL COMMENT '主键',
  `CategoryId` char(36) NOT NULL COMMENT '分类ID',
  `FormId` char(36) NOT NULL COMMENT '表单ID',
  `FlowCode` varchar(50) NOT NULL COMMENT '编码',
  `FlowName` varchar(50) NOT NULL COMMENT '流程名称',
  `FlowContent` text COMMENT '流程JSON',
  `FlowVersion` int(11) NOT NULL DEFAULT '0' COMMENT '流程版本',
  `Memo` varchar(50) DEFAULT NULL COMMENT '备注',
  `Enable` int(11) NOT NULL DEFAULT '1' COMMENT '是否启用',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`FlowId`),
  KEY `CategoryId` (`CategoryId`),
  KEY `FormId` (`FormId`),
  CONSTRAINT `wf_workflow_ibfk_1` FOREIGN KEY (`CategoryId`) REFERENCES `wf_workflow_category` (`Id`),
  CONSTRAINT `wf_workflow_ibfk_2` FOREIGN KEY (`FormId`) REFERENCES `wf_workflow_form` (`FormId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='工作流表';

/*Data for the table `wf_workflow` */

insert  into `wf_workflow`(`FlowId`,`CategoryId`,`FormId`,`FlowCode`,`FlowName`,`FlowContent`,`FlowVersion`,`Memo`,`Enable`,`CreateUserId`,`CreateTime`) values ('011980a7-0ba3-4752-964e-12d88ca5c54c','9e9fc7e7-8792-40f2-97bc-8b42e583126d','3b1ceb38-e1ee-4f15-a709-d6dd3a399c77','15580575818487','员工借款','{\"title\":\"员工借款\",\"nodes\":[{\"name\":\"开始\",\"left\":238,\"top\":29,\"type\":\"start round mix\",\"id\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"部门经理\",\"left\":390,\"top\":93,\"type\":\"task\",\"id\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}},{\"name\":\"财务总监\",\"left\":423,\"top\":213,\"type\":\"task\",\"id\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[]}}},{\"name\":\"结束\",\"left\":694,\"top\":273,\"type\":\"end round\",\"id\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}}],\"lines\":[{\"type\":\"tb\",\"M\":110,\"from\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"to\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"id\":\"b279111d-eb6a-4d8a-86b6-135de84a732a\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":225.5,\"from\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"to\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"id\":\"596c6d67-715e-4332-809b-7a4b8ba7fa50\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":256,\"from\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"to\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"id\":\"f3ddca8b-135b-43f6-b0bc-e42060a233af\",\"name\":\"\",\"dash\":false,\"alt\":true}],\"areas\":[],\"initNum\":8}',0,'测试流程',1,'1',1558057581),('2b1b17c4-69ca-474b-977a-e8b1f1382e89','9e9fc7e7-8792-40f2-97bc-8b42e583126d','fd4a4efc-7df2-49c4-9ffc-f84db346cac7','15601364386520','通知测试','{\"title\":\"通知测试\",\"nodes\":[{\"name\":\"node_1\",\"left\":264,\"top\":83,\"type\":\"start round mix\",\"id\":\"1474e4c4-d512-49aa-8681-8720b4168554\",\"width\":26,\"height\":26,\"alt\":true},{\"name\":\"部门负责人\",\"left\":473,\"top\":84,\"type\":\"task\",\"id\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}},{\"name\":\"wms\",\"left\":480,\"top\":247,\"type\":\"view\",\"id\":\"1f702b3c-b514-47f3-a761-9190e4a8e965\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"CREATEUSER\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"\"}}},{\"name\":\"node_4\",\"left\":837,\"top\":84,\"type\":\"end round\",\"id\":\"06941d43-5d7a-4a4f-a096-1235d493a24c\",\"width\":26,\"height\":26,\"alt\":true}],\"lines\":[{\"type\":\"sl\",\"from\":\"1474e4c4-d512-49aa-8681-8720b4168554\",\"to\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"id\":\"18365384-6f16-41dc-aeb6-ce384cc11d94\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"to\":\"1f702b3c-b514-47f3-a761-9190e4a8e965\",\"id\":\"a543a2e5-9045-4742-b974-477acdd37ffe\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"to\":\"06941d43-5d7a-4a4f-a096-1235d493a24c\",\"id\":\"148f4d46-2ee7-405d-a2e3-270a5b6d2539\",\"name\":\"\",\"dash\":false}],\"areas\":[],\"initNum\":8}',0,'通知测试',1,'1',1560136438),('477e4199-aaf0-4e21-9eed-088922a83d58','9e9fc7e7-8792-40f2-97bc-8b42e583126d','041f7de8-dd83-4aec-a253-e181b77cc40e','15563796431067','员工请假','{\"title\":\"员工请假\",\"nodes\":[{\"name\":\"开始\",\"left\":67,\"top\":44,\"type\":\"start round mix\",\"id\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"王五\",\"left\":438,\"top\":49,\"type\":\"task\",\"id\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"结束\",\"left\":809,\"top\":238,\"type\":\"end round\",\"id\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"张三\",\"left\":778,\"top\":52,\"type\":\"task\",\"id\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"部门负责人\",\"left\":178,\"top\":49,\"type\":\"task\",\"id\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}}],\"lines\":[{\"type\":\"sl\",\"from\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"c764f55f-125b-48e6-8f37-8f281788d960\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"to\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"id\":\"c587ca2a-c95f-491a-b55a-a27c67df3037\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"id\":\"5923dd84-6010-4003-bf4c-d4ee8605e945\",\"setInfo\":{\"CustomSQL\":\"oa_leaveMoreThenThreeDays\"},\"name\":\">3天\",\"dash\":false},{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"id\":\"0ebead88-6942-4563-aa5f-8dbd4c453fe5\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":210.5,\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"ba6765a0-f21e-436a-a094-73a2b03183b1\",\"setInfo\":{\"CustomSQL\":\"oa_leaveLessThenThreeDays\"},\"name\":\"<=3天\",\"dash\":false}],\"areas\":[],\"initNum\":22}',0,'测试流程',1,'1',1556379643),('a9dd987c-f25f-4297-94be-465c5044b076','9e9fc7e7-8792-40f2-97bc-8b42e583126d','4dfacbf1-40bd-4fe0-b4fa-249713f28659','15594687698457','新版测试','{\"title\":\"新版测试\",\"nodes\":[{\"name\":\"开始\",\"left\":282,\"top\":116,\"type\":\"start round mix\",\"id\":\"5231704b-8c9f-4155-9e9d-7cdcdc9c57fe\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"结束\",\"left\":955,\"top\":245,\"type\":\"end round\",\"id\":\"75e7fb37-1d3d-4be6-9e65-aa6ffc78bccf\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"部门负责人\",\"left\":514,\"top\":169,\"type\":\"task\",\"id\":\"d96914cd-d85e-47b4-acdf-1b1fbecd78fc\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}}],\"lines\":[{\"type\":\"sl\",\"from\":\"5231704b-8c9f-4155-9e9d-7cdcdc9c57fe\",\"to\":\"d96914cd-d85e-47b4-acdf-1b1fbecd78fc\",\"id\":\"95aa2ed5-aaf8-410d-a858-c864ca843dfa\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"d96914cd-d85e-47b4-acdf-1b1fbecd78fc\",\"to\":\"75e7fb37-1d3d-4be6-9e65-aa6ffc78bccf\",\"id\":\"bb1567d9-0d70-44e8-a093-a0352fccc700\",\"name\":\"\",\"dash\":false}],\"areas\":[],\"initNum\":6}',0,'新版测试',1,'1',1559468769);

/*Table structure for table `wf_workflow_category` */

DROP TABLE IF EXISTS `wf_workflow_category`;

CREATE TABLE `wf_workflow_category` (
  `Id` char(36) NOT NULL COMMENT '主键',
  `Name` varchar(50) NOT NULL COMMENT '分类名称',
  `ParentId` char(36) DEFAULT NULL COMMENT '父级ID',
  `Memo` varchar(100) DEFAULT NULL COMMENT '备注',
  `Status` int(11) DEFAULT '1' COMMENT '状态',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) DEFAULT NULL COMMENT '创建人',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程分类';

/*Data for the table `wf_workflow_category` */

insert  into `wf_workflow_category`(`Id`,`Name`,`ParentId`,`Memo`,`Status`,`CreateTime`,`CreateUserId`) values ('0b286d49-162a-451b-a1d4-4ab5f2eeddb8','测试分类','00000000-0000-0000-0000-000000000000','你好啊',0,1559180780,'1'),('0e9deb2e-941e-423a-85f0-4092b0c46204','测试分类','00000000-0000-0000-0000-000000000000','你好啊你',0,1559180844,'1'),('9e9fc7e7-8792-40f2-97bc-8b42e583126d','通用流程','00000000-0000-0000-0000-000000000000','通用流程',1,1556072828,'1');

/*Table structure for table `wf_workflow_form` */

DROP TABLE IF EXISTS `wf_workflow_form`;

CREATE TABLE `wf_workflow_form` (
  `FormId` char(36) NOT NULL COMMENT '表单ID',
  `FormName` varchar(50) NOT NULL COMMENT '表单名称',
  `FormType` int(11) DEFAULT NULL COMMENT '表单类型',
  `Content` text COMMENT '表单内容',
  `OriginalContent` text COMMENT '原始表单内容',
  `FormUrl` varchar(200) DEFAULT NULL COMMENT '表单地址',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  PRIMARY KEY (`FormId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程表单';

/*Data for the table `wf_workflow_form` */

insert  into `wf_workflow_form`(`FormId`,`FormName`,`FormType`,`Content`,`OriginalContent`,`FormUrl`,`CreateTime`,`CreateUserId`) values ('041f7de8-dd83-4aec-a253-e181b77cc40e','请假',1,NULL,NULL,'/OA/Leave/Show',1556075724,'1'),('3b1ceb38-e1ee-4f15-a709-d6dd3a399c77','员工借款',0,'\n                                    \n                                    \n                                    \n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">标题：</label>\n		<div class=\"col-sm-9\">\n			<input name=\"FlowParam_1\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款金额：</label>\n		<div class=\"col-sm-9\">\n			  <input name=\"FlowParam_2\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款原因：</label>\n		<div class=\"col-sm-9\">\n			<textarea name=\"FlowParam_4\" class=\"form-control required\"></textarea>\n		</div>\n	</div>\n\n                                \n                                \n                                ','\n                                    \n                                    \n                                    \n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">标题：</label>\n		<div class=\"col-sm-9\">\n			<input type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款金额：</label>\n		<div class=\"col-sm-9\">\n			  <input type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款原因：</label>\n		<div class=\"col-sm-9\">\n			<textarea class=\"form-control required\"></textarea>\n		</div>\n	</div>\n\n                                \n                                \n                                ',NULL,1558057039,'1'),('4dfacbf1-40bd-4fe0-b4fa-249713f28659','新版测试',0,'新版测试\n                                    \n                                ','新版测试\n                                    \n                                ',NULL,1559466362,'1'),('fd4a4efc-7df2-49c4-9ffc-f84db346cac7','通知测试',0,'通知测试\n                                    \n                                ','通知测试\n                                    \n                                ',NULL,1560136422,'1');

/*Table structure for table `wf_workflow_instance` */

DROP TABLE IF EXISTS `wf_workflow_instance`;

CREATE TABLE `wf_workflow_instance` (
  `InstanceId` char(36) NOT NULL COMMENT '实例ID',
  `FlowId` char(36) DEFAULT NULL COMMENT '流程ID',
  `Code` varchar(50) DEFAULT NULL COMMENT '实例编号',
  `ActivityId` char(36) DEFAULT NULL COMMENT '当前节点ID',
  `ActivityType` int(11) DEFAULT NULL COMMENT '当前节点类型',
  `ActivityName` varchar(50) DEFAULT NULL COMMENT '当前节点名称',
  `PreviousId` char(36) DEFAULT NULL COMMENT '上一个节点ID',
  `MakerList` varchar(200) DEFAULT NULL COMMENT '执行人',
  `FlowContent` text COMMENT '流程JSON内容',
  `IsFinish` int(11) DEFAULT NULL COMMENT '流程是否结束',
  `Status` int(11) NOT NULL DEFAULT '0' COMMENT '用户操作状态',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  `CreateUserName` varchar(50) DEFAULT NULL COMMENT '创建人姓名',
  `UpdateTime` bigint(20) NOT NULL COMMENT '实例被修改时间',
  PRIMARY KEY (`InstanceId`),
  KEY `FlowId` (`FlowId`),
  CONSTRAINT `wf_workflow_instance_ibfk_1` FOREIGN KEY (`FlowId`) REFERENCES `wf_workflow` (`FlowId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程实例';

/*Data for the table `wf_workflow_instance` */

insert  into `wf_workflow_instance`(`InstanceId`,`FlowId`,`Code`,`ActivityId`,`ActivityType`,`ActivityName`,`PreviousId`,`MakerList`,`FlowContent`,`IsFinish`,`Status`,`CreateTime`,`CreateUserId`,`CreateUserName`,`UpdateTime`) values ('59e8c3f0-db02-4b11-978a-64bd54ddb6c8','2b1b17c4-69ca-474b-977a-e8b1f1382e89','15602457896721','06941d43-5d7a-4a4f-a096-1235d493a24c',4,'node_4','e0080e39-b227-45c1-81d1-ca18213d80d6','','{\"title\":\"通知测试\",\"nodes\":[{\"name\":\"node_1\",\"left\":264,\"top\":83,\"type\":\"start round mix\",\"id\":\"1474e4c4-d512-49aa-8681-8720b4168554\",\"width\":26,\"height\":26,\"alt\":true},{\"name\":\"部门负责人\",\"left\":473,\"top\":84,\"type\":\"task\",\"id\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}},{\"name\":\"wms\",\"left\":480,\"top\":247,\"type\":\"view\",\"id\":\"1f702b3c-b514-47f3-a761-9190e4a8e965\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"CREATEUSER\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"\"}}},{\"name\":\"node_4\",\"left\":837,\"top\":84,\"type\":\"end round\",\"id\":\"06941d43-5d7a-4a4f-a096-1235d493a24c\",\"width\":26,\"height\":26,\"alt\":true}],\"lines\":[{\"type\":\"sl\",\"from\":\"1474e4c4-d512-49aa-8681-8720b4168554\",\"to\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"id\":\"18365384-6f16-41dc-aeb6-ce384cc11d94\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"to\":\"1f702b3c-b514-47f3-a761-9190e4a8e965\",\"id\":\"a543a2e5-9045-4742-b974-477acdd37ffe\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"to\":\"06941d43-5d7a-4a4f-a096-1235d493a24c\",\"id\":\"148f4d46-2ee7-405d-a2e3-270a5b6d2539\",\"name\":\"\",\"dash\":false}],\"areas\":[],\"initNum\":8}',1,1,1560245789,'1','wms',1560246314),('9be2c4e3-9dc5-43c3-acc0-eb3e388373f5','011980a7-0ba3-4752-964e-12d88ca5c54c','15604151753683','38ebf6f4-5a82-4fb6-9342-94c0f95f6820',4,'结束','d8842622-f5e8-4336-b9cd-4383e5bcec3d','','{\"title\":\"员工借款\",\"nodes\":[{\"name\":\"开始\",\"left\":238,\"top\":29,\"type\":\"start round mix\",\"id\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"部门经理\",\"left\":390,\"top\":93,\"type\":\"task\",\"id\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}},{\"name\":\"财务总监\",\"left\":423,\"top\":213,\"type\":\"task\",\"id\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[]}}},{\"name\":\"结束\",\"left\":694,\"top\":273,\"type\":\"end round\",\"id\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}}],\"lines\":[{\"type\":\"tb\",\"M\":110,\"from\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"to\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"id\":\"b279111d-eb6a-4d8a-86b6-135de84a732a\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":225.5,\"from\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"to\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"id\":\"596c6d67-715e-4332-809b-7a4b8ba7fa50\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":256,\"from\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"to\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"id\":\"f3ddca8b-135b-43f6-b0bc-e42060a233af\",\"name\":\"\",\"dash\":false,\"alt\":true}],\"areas\":[],\"initNum\":8}',1,1,1560415175,'1','wms',1560415232),('a32a6150-e4ae-4264-aeb8-93a89638679c','477e4199-aaf0-4e21-9eed-088922a83d58','15604170867061','6dae3d55-04fc-4112-824f-e87542a03779',4,'结束','634b9765-ac0e-4272-bfab-f5b260c7fde8','','{\"title\":\"员工请假\",\"nodes\":[{\"name\":\"开始\",\"left\":67,\"top\":44,\"type\":\"start round mix\",\"id\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"王五\",\"left\":438,\"top\":49,\"type\":\"task\",\"id\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"结束\",\"left\":809,\"top\":238,\"type\":\"end round\",\"id\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"张三\",\"left\":778,\"top\":52,\"type\":\"task\",\"id\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"部门负责人\",\"left\":178,\"top\":49,\"type\":\"task\",\"id\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}}],\"lines\":[{\"type\":\"sl\",\"from\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"c764f55f-125b-48e6-8f37-8f281788d960\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"to\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"id\":\"c587ca2a-c95f-491a-b55a-a27c67df3037\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"id\":\"5923dd84-6010-4003-bf4c-d4ee8605e945\",\"setInfo\":{\"CustomSQL\":\"oa_leaveMoreThenThreeDays\"},\"name\":\">3天\",\"dash\":false},{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"id\":\"0ebead88-6942-4563-aa5f-8dbd4c453fe5\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":210.5,\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"ba6765a0-f21e-436a-a094-73a2b03183b1\",\"setInfo\":{\"CustomSQL\":\"oa_leaveLessThenThreeDays\"},\"name\":\"<=3天\",\"dash\":false}],\"areas\":[],\"initNum\":22}',1,1,1560417086,'1','wms',1560417476),('d8420a67-c3f4-4f93-a5da-a5598094f447','477e4199-aaf0-4e21-9eed-088922a83d58','15604180620754','6dae3d55-04fc-4112-824f-e87542a03779',4,'结束','77825e68-4a61-43b8-9081-504088b332e6','','{\"title\":\"员工请假\",\"nodes\":[{\"name\":\"开始\",\"left\":67,\"top\":44,\"type\":\"start round mix\",\"id\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"王五\",\"left\":438,\"top\":49,\"type\":\"task\",\"id\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"结束\",\"left\":809,\"top\":238,\"type\":\"end round\",\"id\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"张三\",\"left\":778,\"top\":52,\"type\":\"task\",\"id\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"部门负责人\",\"left\":178,\"top\":49,\"type\":\"task\",\"id\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}}],\"lines\":[{\"type\":\"sl\",\"from\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"c764f55f-125b-48e6-8f37-8f281788d960\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"to\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"id\":\"c587ca2a-c95f-491a-b55a-a27c67df3037\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"id\":\"5923dd84-6010-4003-bf4c-d4ee8605e945\",\"setInfo\":{\"CustomSQL\":\"oa_leaveMoreThenThreeDays\"},\"name\":\">3天\",\"dash\":false},{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"id\":\"0ebead88-6942-4563-aa5f-8dbd4c453fe5\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":210.5,\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"ba6765a0-f21e-436a-a094-73a2b03183b1\",\"setInfo\":{\"CustomSQL\":\"oa_leaveLessThenThreeDays\"},\"name\":\"<=3天\",\"dash\":false}],\"areas\":[],\"initNum\":22}',1,1,1560418062,'1','wms',1560419404);

/*Table structure for table `wf_workflow_instance_form` */

DROP TABLE IF EXISTS `wf_workflow_instance_form`;

CREATE TABLE `wf_workflow_instance_form` (
  `Id` char(36) NOT NULL COMMENT '主键',
  `InstanceId` char(36) DEFAULT NULL COMMENT '流程实例ID',
  `FormId` char(36) DEFAULT NULL COMMENT '表单ID',
  `FormContent` text COMMENT '表单内容/对于表单ID',
  `FormType` int(11) DEFAULT NULL COMMENT '表单类型',
  `FormUrl` varchar(200) DEFAULT NULL COMMENT '表单地址',
  `FormData` text COMMENT '表单数据JSON',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`),
  KEY `InstanceId` (`InstanceId`),
  KEY `FormId` (`FormId`),
  CONSTRAINT `wf_workflow_instance_form_ibfk_1` FOREIGN KEY (`InstanceId`) REFERENCES `wf_workflow_instance` (`InstanceId`),
  CONSTRAINT `wf_workflow_instance_form_ibfk_2` FOREIGN KEY (`FormId`) REFERENCES `wf_workflow_form` (`FormId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程实例表单关联表';

/*Data for the table `wf_workflow_instance_form` */

insert  into `wf_workflow_instance_form`(`Id`,`InstanceId`,`FormId`,`FormContent`,`FormType`,`FormUrl`,`FormData`,`CreateUserId`,`CreateTime`) values ('6432959b-620f-4fbe-9f69-9b851574810f','9be2c4e3-9dc5-43c3-acc0-eb3e388373f5','3b1ceb38-e1ee-4f15-a709-d6dd3a399c77','\n                                    \n                                    \n                                    \n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">标题：</label>\n		<div class=\"col-sm-9\">\n			<input name=\"FlowParam_1\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款金额：</label>\n		<div class=\"col-sm-9\">\n			  <input name=\"FlowParam_2\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款原因：</label>\n		<div class=\"col-sm-9\">\n			<textarea name=\"FlowParam_4\" class=\"form-control required\"></textarea>\n		</div>\n	</div>\n\n                                \n                                \n                                ',0,NULL,'{\"FlowParam_1\":\"测试\",\"FlowParam_2\":\"100\",\"FlowParam_4\":\"测试\"}','1',1560415175),('69ad72e6-99cc-4a00-8bc4-88025e141cff','a32a6150-e4ae-4264-aeb8-93a89638679c','041f7de8-dd83-4aec-a253-e181b77cc40e','1',1,'/OA/Leave/Show','1','1',1560417087),('b744d9b4-70b3-4151-b67b-fd2efc952369','d8420a67-c3f4-4f93-a5da-a5598094f447','041f7de8-dd83-4aec-a253-e181b77cc40e','2',1,'/OA/Leave/Show','2','1',1560418062),('f938467a-b223-417a-b593-853d5b6ee0e4','59e8c3f0-db02-4b11-978a-64bd54ddb6c8','fd4a4efc-7df2-49c4-9ffc-f84db346cac7','通知测试\n                                    \n                                ',0,NULL,'{}','1',1560245789);

/*Table structure for table `wf_workflow_notice` */

DROP TABLE IF EXISTS `wf_workflow_notice`;

CREATE TABLE `wf_workflow_notice` (
  `Id` char(36) NOT NULL COMMENT '主键',
  `InstanceId` char(36) NOT NULL COMMENT '流程实例ID',
  `NodeId` char(36) NOT NULL COMMENT '通知节点ID',
  `NodeName` varchar(100) DEFAULT NULL COMMENT '通知节点名称',
  `Maker` varchar(50) NOT NULL COMMENT '执行人',
  `IsTransition` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否已经流转过',
  `Status` tinyint(4) NOT NULL DEFAULT '1' COMMENT '状态，退回时候用',
  `IsRead` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否已阅',
  PRIMARY KEY (`Id`),
  KEY `InstanceId` (`InstanceId`),
  CONSTRAINT `wf_workflow_notice_ibfk_1` FOREIGN KEY (`InstanceId`) REFERENCES `wf_workflow_instance` (`InstanceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程通知节点表';

/*Data for the table `wf_workflow_notice` */

insert  into `wf_workflow_notice`(`Id`,`InstanceId`,`NodeId`,`NodeName`,`Maker`,`IsTransition`,`Status`,`IsRead`) values ('1d6af7db-3117-46b9-a5dd-8ba0cb9514b6','59e8c3f0-db02-4b11-978a-64bd54ddb6c8','1f702b3c-b514-47f3-a761-9190e4a8e965','wms','1',1,1,1);

/*Table structure for table `wf_workflow_operation_history` */

DROP TABLE IF EXISTS `wf_workflow_operation_history`;

CREATE TABLE `wf_workflow_operation_history` (
  `OperationId` char(36) NOT NULL COMMENT '主键',
  `InstanceId` char(36) DEFAULT NULL COMMENT '实例进程ID',
  `NodeId` char(36) DEFAULT NULL COMMENT '节点ID',
  `NodeName` varchar(200) DEFAULT NULL COMMENT '节点名称',
  `TransitionType` int(11) DEFAULT NULL COMMENT '流转类型',
  `Content` varchar(500) DEFAULT NULL COMMENT '操作内容',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  `CreateUserName` varchar(50) DEFAULT NULL COMMENT '创建人姓名',
  PRIMARY KEY (`OperationId`),
  KEY `InstanceId` (`InstanceId`),
  CONSTRAINT `wf_workflow_operation_history_ibfk_1` FOREIGN KEY (`InstanceId`) REFERENCES `wf_workflow_instance` (`InstanceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程操作历史记录';

/*Data for the table `wf_workflow_operation_history` */

insert  into `wf_workflow_operation_history`(`OperationId`,`InstanceId`,`NodeId`,`NodeName`,`TransitionType`,`Content`,`CreateTime`,`CreateUserId`,`CreateUserName`) values ('21d8c76f-d6bf-479c-9fa4-68e67a8bf57d','a32a6150-e4ae-4264-aeb8-93a89638679c','33e53484-5b48-4210-a62c-949dd7d6dbaa','部门负责人',2,'同意！',1560417353,'1','wms'),('28e938aa-6d37-41a5-a282-5a8d44eac04b','a32a6150-e4ae-4264-aeb8-93a89638679c','5fb04da8-7113-4f80-9c91-be19db2c1a9c','王五',2,'同意！',1560417448,'4','王五123'),('3530b628-0493-4562-bdd8-105039cdbac8','59e8c3f0-db02-4b11-978a-64bd54ddb6c8','1474e4c4-d512-49aa-8681-8720b4168554','node_1',1,'提交流程',1560245806,'1','wms'),('4606236e-e153-4132-85c7-cd0957fbac23','59e8c3f0-db02-4b11-978a-64bd54ddb6c8','1f702b3c-b514-47f3-a761-9190e4a8e965','wms',9,'流程已阅',1560301605,'1','wms'),('672584ec-6f97-4613-b286-863f0fae05de','59e8c3f0-db02-4b11-978a-64bd54ddb6c8','e0080e39-b227-45c1-81d1-ca18213d80d6','部门负责人',2,'同意！',1560246314,'1','wms'),('68ad122c-ccf6-4e66-aa0f-888db2f0e58d','9be2c4e3-9dc5-43c3-acc0-eb3e388373f5','f9104625-252a-49c8-91d4-9401509fceb5','开始',1,'提交流程',1560415177,'1','wms'),('8e5b642c-780c-4c15-bfa8-1962446cbb6b','a32a6150-e4ae-4264-aeb8-93a89638679c','77825e68-4a61-43b8-9081-504088b332e6','开始',1,'提交流程',1560417087,'1','wms'),('9ce5e7d3-4729-4906-a166-65e55a1372bd','a32a6150-e4ae-4264-aeb8-93a89638679c','634b9765-ac0e-4272-bfab-f5b260c7fde8','张三',2,'同意！',1560417476,'5','张三'),('a45896aa-daa0-4d79-8da8-e21205dbbc0e','d8420a67-c3f4-4f93-a5da-a5598094f447','33e53484-5b48-4210-a62c-949dd7d6dbaa','部门负责人',2,'同意！',1560419404,'1','wms'),('cc601b1c-6ca4-48c8-970f-f0336a6f5152','9be2c4e3-9dc5-43c3-acc0-eb3e388373f5','f5cef31d-cb13-4195-86f3-7e2c96f345ee','部门经理',2,'同意！',1560415190,'1','wms'),('fcf9bdd4-5f22-4ea3-afec-bb492e603961','d8420a67-c3f4-4f93-a5da-a5598094f447','77825e68-4a61-43b8-9081-504088b332e6','开始',1,'提交流程',1560418062,'1','wms'),('fe1adecd-1985-48b0-b888-55e3cff873d7','9be2c4e3-9dc5-43c3-acc0-eb3e388373f5','d8842622-f5e8-4336-b9cd-4383e5bcec3d','财务总监',2,'同意！',1560415232,'5','张三');

/*Table structure for table `wf_workflow_transition_history` */

DROP TABLE IF EXISTS `wf_workflow_transition_history`;

CREATE TABLE `wf_workflow_transition_history` (
  `TransitionId` char(36) NOT NULL COMMENT '主键',
  `InstanceId` char(36) DEFAULT NULL COMMENT '流程实例ID',
  `FromNodeId` char(36) DEFAULT NULL COMMENT '开始节点Id',
  `FromNodeType` int(11) DEFAULT NULL COMMENT '开始节点类型',
  `FromNodName` varchar(50) DEFAULT NULL COMMENT '开始节点名称',
  `ToNodeId` char(36) DEFAULT NULL COMMENT '结束节点Id',
  `ToNodeType` int(11) DEFAULT NULL COMMENT '结束节点类型',
  `ToNodeName` varchar(50) DEFAULT NULL COMMENT '结束节点名称',
  `TransitionState` int(11) DEFAULT NULL COMMENT '转化状态',
  `IsFinish` int(11) DEFAULT NULL COMMENT '是否结束',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  `CreateUserName` varchar(50) DEFAULT NULL COMMENT '创建人姓名',
  PRIMARY KEY (`TransitionId`),
  KEY `InstanceId` (`InstanceId`),
  CONSTRAINT `wf_workflow_transition_history_ibfk_1` FOREIGN KEY (`InstanceId`) REFERENCES `wf_workflow_instance` (`InstanceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程流转历史表';

/*Data for the table `wf_workflow_transition_history` */

insert  into `wf_workflow_transition_history`(`TransitionId`,`InstanceId`,`FromNodeId`,`FromNodeType`,`FromNodName`,`ToNodeId`,`ToNodeType`,`ToNodeName`,`TransitionState`,`IsFinish`,`CreateTime`,`CreateUserId`,`CreateUserName`) values ('066307c9-640e-4bb0-911d-766a3cb024ab','d8420a67-c3f4-4f93-a5da-a5598094f447','77825e68-4a61-43b8-9081-504088b332e6',3,'开始','33e53484-5b48-4210-a62c-949dd7d6dbaa',2,'部门负责人',0,0,1560418062,'1','wms'),('10f1968e-1f80-4c3d-8e93-5f13835c2618','9be2c4e3-9dc5-43c3-acc0-eb3e388373f5','f9104625-252a-49c8-91d4-9401509fceb5',3,'开始','f5cef31d-cb13-4195-86f3-7e2c96f345ee',2,'部门经理',0,0,1560415177,'1','wms'),('28480957-8b93-4d9f-948c-3862f42b18a8','a32a6150-e4ae-4264-aeb8-93a89638679c','33e53484-5b48-4210-a62c-949dd7d6dbaa',2,'部门负责人','5fb04da8-7113-4f80-9c91-be19db2c1a9c',2,'王五',0,0,1560417353,'1','wms'),('5c0c3c95-31fe-4a1c-99a4-a045999339a2','9be2c4e3-9dc5-43c3-acc0-eb3e388373f5','f5cef31d-cb13-4195-86f3-7e2c96f345ee',2,'部门经理','d8842622-f5e8-4336-b9cd-4383e5bcec3d',2,'财务总监',0,0,1560415190,'1','wms'),('653192e9-e944-434f-9f40-c6693d5df821','a32a6150-e4ae-4264-aeb8-93a89638679c','5fb04da8-7113-4f80-9c91-be19db2c1a9c',2,'王五','634b9765-ac0e-4272-bfab-f5b260c7fde8',2,'张三',0,0,1560417448,'4','王五123'),('65bf952f-9594-4392-882f-6f6580519f5b','59e8c3f0-db02-4b11-978a-64bd54ddb6c8','e0080e39-b227-45c1-81d1-ca18213d80d6',2,'部门负责人','06941d43-5d7a-4a4f-a096-1235d493a24c',4,'node_4',0,1,1560246314,'1','wms'),('8da53ca8-a71f-4ee3-a7db-e7d673860e6c','a32a6150-e4ae-4264-aeb8-93a89638679c','634b9765-ac0e-4272-bfab-f5b260c7fde8',2,'张三','6dae3d55-04fc-4112-824f-e87542a03779',4,'结束',0,1,1560417476,'5','张三'),('b5125db1-a882-464e-9d5a-a4df3319d976','d8420a67-c3f4-4f93-a5da-a5598094f447','33e53484-5b48-4210-a62c-949dd7d6dbaa',2,'部门负责人','6dae3d55-04fc-4112-824f-e87542a03779',4,'结束',0,1,1560419404,'1','wms'),('c03193be-6a26-4ef3-ae3c-0f840f03152e','9be2c4e3-9dc5-43c3-acc0-eb3e388373f5','d8842622-f5e8-4336-b9cd-4383e5bcec3d',2,'财务总监','38ebf6f4-5a82-4fb6-9342-94c0f95f6820',4,'结束',0,1,1560415232,'5','张三'),('c07e31a5-4f8d-465f-8586-4d87adf12be4','a32a6150-e4ae-4264-aeb8-93a89638679c','77825e68-4a61-43b8-9081-504088b332e6',3,'开始','33e53484-5b48-4210-a62c-949dd7d6dbaa',2,'部门负责人',0,0,1560417087,'1','wms'),('e2daf996-cf1a-4ed0-8b40-22f6095499b5','59e8c3f0-db02-4b11-978a-64bd54ddb6c8','1474e4c4-d512-49aa-8681-8720b4168554',3,'node_1','e0080e39-b227-45c1-81d1-ca18213d80d6',2,'部门负责人',0,0,1560245806,'1','wms');

/*Table structure for table `wf_workflowsql` */

DROP TABLE IF EXISTS `wf_workflowsql`;

CREATE TABLE `wf_workflowsql` (
  `Name` varchar(50) NOT NULL COMMENT '流程sql名称,必须是以wf_为开头，用于判断属于哪个系统，方便调用接口',
  `FlowSQL` text NOT NULL COMMENT '流程SQL，执行结果必须是一行一列',
  `Param` varchar(200) DEFAULT NULL COMMENT '参数名。以英文 , 分割',
  `SQLType` tinyint(4) NOT NULL DEFAULT '0' COMMENT '类型，0：选人节点，必须返回的是用户ID，1：连线条件，条件通过返回的是一行一列的数据，不通过没有任何返回结果',
  `Status` int(11) NOT NULL DEFAULT '1' COMMENT '状态',
  `Remark` varchar(200) DEFAULT NULL COMMENT '备注',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  PRIMARY KEY (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用于工作流获取权限系统数据';

/*Data for the table `wf_workflowsql` */

insert  into `wf_workflowsql`(`Name`,`FlowSQL`,`Param`,`SQLType`,`Status`,`Remark`,`CreateTime`,`CreateUserId`) values ('wf_agree','SELECT 1 ',NULL,0,1,'同意、通过',1,1),('wf_deprecate','SELECT 0 ',NULL,0,1,'不同意、不通过',1,1);

/*Table structure for table `wx_account` */

DROP TABLE IF EXISTS `wx_account`;

CREATE TABLE `wx_account` (
  `WeixinId` varchar(50) NOT NULL COMMENT '微信原始Id',
  `AppId` varchar(50) NOT NULL COMMENT '开发者ID',
  `AppSecret` varchar(200) NOT NULL COMMENT '开发者密码',
  `WeixinName` varchar(50) NOT NULL COMMENT '公众号名称',
  `WeixinQRCode` varchar(500) DEFAULT NULL COMMENT '公众号二维码地址',
  `AccessToken` varchar(1000) DEFAULT NULL COMMENT 'AccessToken',
  `AccessTokenCreateTime` datetime DEFAULT NULL COMMENT 'AccessToken创建时间',
  `JsApiTicket` varchar(100) DEFAULT NULL COMMENT 'JS API临时票据',
  `JsApiTicketCreateTime` datetime DEFAULT NULL COMMENT 'JS API临时票据创建时间',
  `SubscribePageUrl` varchar(500) DEFAULT NULL COMMENT '微信号关注引导页地址URL',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `Token` varchar(50) NOT NULL COMMENT '令牌',
  `EncodingAESKey` varchar(200) DEFAULT NULL COMMENT '消息加解密密钥',
  PRIMARY KEY (`WeixinId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='公众号表';

/*Data for the table `wx_account` */

insert  into `wx_account`(`WeixinId`,`AppId`,`AppSecret`,`WeixinName`,`WeixinQRCode`,`AccessToken`,`AccessTokenCreateTime`,`JsApiTicket`,`JsApiTicketCreateTime`,`SubscribePageUrl`,`CreateTime`,`Token`,`EncodingAESKey`) values ('gh_3f66fb647ff1','wxeb8c08a03de853d5','485c8b1eeb798401c9af06798f91ec6c','自由reading',NULL,'17_ESnucPG7Ccpi7_t7AfPIUMzUN2v9Wi2uDYlsJRyh4Ho7bivZh9AmoIkXjTXZYUKcTyP1IOo7WifJJiyjnRfAnRaDpyv0zYO4OFiSAUYLGF-pVAAV5vZcDREipUIole9JwQVmH07mOo7yKO6NUOViABALFM','2019-01-12 10:29:53',NULL,NULL,NULL,1546912897,'wangmaosheng','w9jBowtJ9rqgzcHFFyJ0VakWDdNfknphEmFB7pocglk');

/*Table structure for table `wx_account_miniprogram` */

DROP TABLE IF EXISTS `wx_account_miniprogram`;

CREATE TABLE `wx_account_miniprogram` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `WeixinId` varchar(50) NOT NULL COMMENT '微信原始Id',
  `AppId` varchar(50) NOT NULL COMMENT 'AppID(小程序ID)',
  `AppSecret` varchar(200) NOT NULL COMMENT 'AppSecret(小程序密钥)',
  PRIMARY KEY (`Id`),
  KEY `WeixinId` (`WeixinId`),
  CONSTRAINT `wx_account_miniprogram_ibfk_1` FOREIGN KEY (`WeixinId`) REFERENCES `wx_account` (`WeixinId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `wx_account_miniprogram` */

insert  into `wx_account_miniprogram`(`Id`,`WeixinId`,`AppId`,`AppSecret`) values (1,'gh_3f66fb647ff1','wx20a06c30c8b81e61','6223a1e06f10ebc8ae9fe9eed65042bb');

/*Table structure for table `wx_keyword` */

DROP TABLE IF EXISTS `wx_keyword`;

CREATE TABLE `wx_keyword` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleId` int(11) NOT NULL COMMENT '规则表ID',
  `Keyword` varchar(50) NOT NULL COMMENT '关键字',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`),
  KEY `RuleId` (`RuleId`),
  CONSTRAINT `wx_keyword_ibfk_1` FOREIGN KEY (`RuleId`) REFERENCES `wx_rule` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='关键字表';

/*Data for the table `wx_keyword` */

insert  into `wx_keyword`(`Id`,`RuleId`,`Keyword`,`CreateTime`) values (1,3,'妹子',1560356237);

/*Table structure for table `wx_menu` */

DROP TABLE IF EXISTS `wx_menu`;

CREATE TABLE `wx_menu` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ParentId` int(11) NOT NULL DEFAULT '0' COMMENT '父级ID',
  `Name` varchar(60) NOT NULL COMMENT '菜单标题，不超过16个字节，子菜单不超过60个字节',
  `Type` varchar(50) DEFAULT NULL COMMENT '菜单的响应动作类型，view表示网页类型，click表示点击类型，miniprogram表示小程序类型',
  `Key` varchar(128) DEFAULT NULL COMMENT '菜单KEY值，用于消息接口推送，不超过128字节',
  `Url` varchar(1000) DEFAULT NULL COMMENT '网页 链接，用户点击菜单可打开链接，不超过1024字节。 type为miniprogram时，不支持小程序的老版本客户端将打开本url。',
  `AppId` varchar(50) DEFAULT NULL COMMENT '小程序的appid（仅认证公众号可配置）',
  `PagePath` varchar(200) DEFAULT NULL COMMENT '小程序的页面路径',
  `Sort` int(11) NOT NULL DEFAULT '0',
  `IsDel` int(11) NOT NULL DEFAULT '0',
  `CreateTime` bigint(20) DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='微信菜单';

/*Data for the table `wx_menu` */

/*Table structure for table `wx_miniprogram_user` */

DROP TABLE IF EXISTS `wx_miniprogram_user`;

CREATE TABLE `wx_miniprogram_user` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `OpenId` varchar(50) NOT NULL COMMENT '小程序对应该用户的OpenId',
  `UnionId` varchar(200) DEFAULT NULL COMMENT '微信开放平台的唯一标识符',
  `NickName` varchar(50) DEFAULT NULL COMMENT '昵称',
  `City` varchar(50) DEFAULT NULL COMMENT '用户所在城市',
  `Country` varchar(50) DEFAULT NULL COMMENT '用户所在国家',
  `Province` varchar(50) DEFAULT NULL COMMENT '用户所在的省份',
  `Language` varchar(20) DEFAULT NULL COMMENT '语言',
  `Gender` tinyint(4) DEFAULT NULL COMMENT '性别',
  `AvatarUrl` varchar(1000) DEFAULT NULL COMMENT '头像',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`OpenId`,`Id`),
  KEY `Id` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='小程序用户表';

/*Data for the table `wx_miniprogram_user` */

insert  into `wx_miniprogram_user`(`Id`,`OpenId`,`UnionId`,`NickName`,`City`,`Country`,`Province`,`Language`,`Gender`,`AvatarUrl`,`CreateTime`) values (1,'ofovX1PhIU2fMvagdO1wtJj2U8Bc',NULL,'岁月静好','Lianyungang','China','Jiangsu','zh_CN',1,'https://wx.qlogo.cn/mmopen/vi_32/OmJum4poEd64ibQjFZ4DWqND7OtN2ia9akuYSn9fxWrwT4PaH71mqr7ls3jdYHDlj10Oq8vS9pjw27WjebgMYWWA/132',1555578279);

/*Table structure for table `wx_news_response` */

DROP TABLE IF EXISTS `wx_news_response`;

CREATE TABLE `wx_news_response` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleId` int(11) NOT NULL COMMENT '规则ID',
  `Title` varchar(50) DEFAULT NULL COMMENT '图文消息标题',
  `Description` varchar(200) DEFAULT NULL COMMENT '图文消息描述',
  `PicUrl` varchar(500) DEFAULT NULL COMMENT '图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200',
  `Url` varchar(500) DEFAULT NULL COMMENT '点击图文消息跳转链接',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `IsDel` tinyint(1) NOT NULL DEFAULT '0' COMMENT '是否已删除0否，1是',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`),
  KEY `RuleId` (`RuleId`),
  CONSTRAINT `wx_news_response_ibfk_1` FOREIGN KEY (`RuleId`) REFERENCES `wx_rule` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='回复图文消息';

/*Data for the table `wx_news_response` */

/*Table structure for table `wx_rule` */

DROP TABLE IF EXISTS `wx_rule`;

CREATE TABLE `wx_rule` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleName` varchar(50) NOT NULL COMMENT '规则名称',
  `RuleType` tinyint(4) NOT NULL COMMENT '规则类型0:普通，1:未匹配到回复规则',
  `RequestMsgType` int(11) NOT NULL COMMENT '规则类型',
  `ResponseMsgType` int(11) NOT NULL COMMENT '响应消息类型',
  `CreateTime` bigint(20) NOT NULL COMMENT '规则创建时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='响应微信消息规则';

/*Data for the table `wx_rule` */

insert  into `wx_rule`(`Id`,`RuleName`,`RuleType`,`RequestMsgType`,`ResponseMsgType`,`CreateTime`) values (1,'自动回复',2,1,1,1547103227),(2,'关注回复',1,8,1,1547112830),(3,'求妹子',0,1,1,1560356237);

/*Table structure for table `wx_text_response` */

DROP TABLE IF EXISTS `wx_text_response`;

CREATE TABLE `wx_text_response` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleId` int(11) NOT NULL COMMENT '规则ID',
  `Content` varchar(1000) DEFAULT NULL COMMENT '消息内容',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`),
  KEY `RuleId` (`RuleId`),
  CONSTRAINT `wx_text_response_ibfk_1` FOREIGN KEY (`RuleId`) REFERENCES `wx_rule` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='文本回复';

/*Data for the table `wx_text_response` */

insert  into `wx_text_response`(`Id`,`RuleId`,`Content`,`CreateTime`) values (1,1,'亲，您发的我识别不了啊',1547103227),(2,2,'终于等到你了~~',1547112831),(3,3,'我就是',1560356237);

/*Table structure for table `wx_user` */

DROP TABLE IF EXISTS `wx_user`;

CREATE TABLE `wx_user` (
  `OpenId` varchar(50) NOT NULL COMMENT '用户OpenId主键',
  `Subscribe` int(11) DEFAULT NULL COMMENT '用户是否关注该公众号1：关注了，0：没关注',
  `UserName` varchar(100) DEFAULT NULL COMMENT '用户名',
  `NickName` varchar(100) DEFAULT NULL COMMENT '昵称',
  `Mobile` varchar(50) DEFAULT NULL COMMENT '手机号码',
  `Sex` int(11) DEFAULT NULL COMMENT '用户的性别，值为1时是男性，值为2时是女性，值为0时是未知',
  `City` varchar(50) DEFAULT NULL COMMENT '用户所在城市',
  `Country` varchar(50) DEFAULT NULL COMMENT '用户所在国家',
  `Province` varchar(50) DEFAULT NULL COMMENT '用户所在的省份',
  `Headimgurl` varchar(500) DEFAULT NULL COMMENT '用户头像',
  `Birthday` varchar(50) DEFAULT NULL COMMENT '生日',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `IsSync` int(11) NOT NULL DEFAULT '0' COMMENT '用户基本信息是否同步过',
  PRIMARY KEY (`OpenId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='微信用户表';

/*Data for the table `wx_user` */

insert  into `wx_user`(`OpenId`,`Subscribe`,`UserName`,`NickName`,`Mobile`,`Sex`,`City`,`Country`,`Province`,`Headimgurl`,`Birthday`,`CreateTime`,`IsSync`) values ('ouUD4sr2rk1MT2UqTTqpGNFot-GI',1,NULL,'不覊之士',NULL,1,'连云港','中国','江苏','http://thirdwx.qlogo.cn/mmopen/eDicfvb2fvAYRduibVLeexKc0ZdJ67jryCbibbwWwTaypS2G5rdajCzRzq4QKbYVazuoe2Z9VTNlTzB6O6xOSep0cB24cfgPOXQ/132',NULL,1547123099,1);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
