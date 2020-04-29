module.exports = {
  version: "1.0.0",
  note: '小程序转发到微信群，可获赠积分',
  subDomain: "http://127.0.0.1", // 域名
  appid: "wx35c1ec834a7db91c",
  shareProfile: '欢迎使用 MS BPM', // 首页转发的时候话术
  gateway: { //网关信息
    client_id: "mssystem",
    client_secret: "123",
    grant_type: "client_credentials",
    scopes: "mssystem_api",
    url: "http://192.168.0.200:5000",
    tokenurl: "/connect/token",
    tokenkey: 'GATEWAYTOKEN',
    tokenTimeKey: "GATEWAYTOKENexpiredTime",
  }
}
