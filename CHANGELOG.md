### 1.8.1.3

2020-06-19

FEATURE:
- 升级支付SDK版本.
- 升级 Facebook Audience Network SDK 版本到5.9.0

CHANGES:
1. 使用最新  [mainTemplate.gradle](https://github.com/sunitsdk/SUnitUnityDemo/blob/master_181/Assets/Plugins/Android/mainTemplate.gradle)
2. gameLevelEnd 增加是否过关参数


### 1.6.2.1
2020-6-4

- 升级到1.6.2.1不需要特殊的步骤

- 新增[评分引导功能](https://github.com/sunitsdk/SUnitUnityDemo/wiki/%E8%AF%84%E5%88%86%E5%BC%95%E5%AF%BC%EF%BC%88%E5%8F%AF%E9%80%89%E5%8A%9F%E8%83%BD%29)

### [重要]1.5.1.3
2020-05-12
- 在第一个 Awake 时主动申请sd卡权限(方法内含有Id初始化相关策略，即使接入方自己申请了权限也需要调用此方法)
    * Unity：SHAREitSDK.SHAREitSDK.requestStoragePermissions();
- 使用最新  [mainTemplate.gradle](https://github.com/sunitsdk/SUnitUnityDemo/blob/master_151/Assets/Plugins/Android/mainTemplate.gradle) 文件
- 在mainTemplate.gradle中applicationId下方增加account_type、content_authority

```
 defaultConfig {
         applicationId '**APPLICATIONID**'
         ......
        //SHAREit begin
          multiDexEnabled true
          resValue "string", "account_type", "${applicationId}.type"
          resValue "string", "content_authority", "${applicationId}.provider"
        //SHAREit end
    }
 ```
### [里程碑版本]1.3.1.1
2020-04-20
1. 升级 androidx 版本
2. 升级广告源版本: admob facebook mopub applovin unityAds
3. 增加 admob 聚合 applovin, admob 聚合 unityAds

接入改动(替换 [mainTemplate.gradle](https://github.com/sunitsdk/SUnitUnityDemo/blob/master_130/Assets/Plugins/Android/mainTemplate.gradle) 文件:

### 1.2.6.0

2020-03-29

1. 修复一处埋点上报问题

### 1.2.5

2020-03-20
升级到1.2.5不需要特殊的步骤

### 1.2.4
2020-03-11
1. 更新 pay 模块, 修复bug
2. 修复 Applovin Banner 请求 API
3. 优化 Banner 广告逻辑, 内部优化
4. 增加悬浮球功能

### 1.2.3.6

2020-03-03

升级到1.2.3.6不需要特殊的步骤

### 1.2.2

2020-02-17

1. 优化 consumer proguard file

### 1.2.1.2
2020-02-14

1. 增加 'load_strategy' = 3, 顺序延迟提前返显

### 1.2.0.4

2020-02-12
1. 统一子sdk的版本号

### 1.2.0

2020-02-11

1. 更改为 maven 依赖方式
2. 针对 1.1.2.3 版本修复埋点bug

### 1.1.2.3

2020-01-19

1. 插屏广告isAdReady 增加方法说明
2. 激励视频广告 isAdReady 增加子入口参数和说明
3. 广告数据上报 showRewardedBadgeView 增加子入口参数和说明

### 1.1.1.3

2020-02-11

升级到1.1.1.1不需要特殊的步骤

### 1.1.1.1

2020-01-13

升级到1.1.1.1不需要特殊的步骤

### 1.1.0.1

2020-01-08

升级到1.1.0.1不需要特殊的步骤

### 1.0.9.2

2020-01-07

1. 预埋广告配置
2. 添加默认广告配置文件
3. 删除自动申请权限方式，提供申请权限方法

### 1.0.8.8

2019-12-31

1. 3.5修改初始化，读取配置文件自动初始化
2. 4.1此版本移除gameStart和gameEnd事件上报
3. 6.3 增加自动申请权限方式

### 1.0.8.4

2019-12-23

1. 3.3广告源的SDK Network依赖(Mopub)
2. 4.3.5 增加banner说明

### 1.0.4.0

2019-11-04

1. 接口参数说明
2. 注意事项里读写权限授权相关问题

### 1.0.1.0

2019-10-22

1. AppID必须要申请
2. 登录增加横屏适配
3. 对isAdReady进行说明
4. 对showAd进行说明
5. Fix bug

### 1.0.0.0

2019-09-10

1. 初始文档
