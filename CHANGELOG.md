#### 1.7.0

2020-06-03

1. 新增接入Adjust SDK


接入改动:使用最新  [mainTemplate.gradle](https://github.com/sunitsdk/SUnitUnityDemo/blob/master_1.7.0/Assets/Plugins/Android/mainTemplate.gradle) 文件

1. 添加接入Adjust SDK版本为

```
    ext {
        //修改版本为1.7.0.1  如果接入adjust sdk，测试阶段需使用 VERSION_SUNIT = "1.7.0.1-sandbox-env" 使用adjust的sandbox环境
        VERSION_SUNIT = "1.7.0.1"

        VERSION_ADCOLONY_AD="4.1.0"
        VERSION_ADMOB_AD = "18.3.0"
        VERSION_ADMOB_M_FB = "5.6.1.0"
        VERSION_ADMOB_M_MOPUB = "5.10.0.0"
        VERSION_ADMOB_M_UNITYADS = "3.4.2.0"
        VERSION_ADMOB_M_APPLOVIN = "9.11.4.0"

        VERSION_APPLOVIN_AD = "9.11.4"
        VERSION_FACEBOOK_AD = "5.6.1"
        VERSION_FYBER_AD = "7.3.4"
        VERSION_IRONSOURCE_AD = "6.10.2"
        VERSION_UNITY_ADS_AD = "3.4.2"
        VERSION_VUNGLE_AD = "6.4.11"
        VERSION_MOPUB_AD = "5.10.0"
        VERSION_MOPUB_M_ADMOB = "18.3.0.3"
        VERSION_MOPUB_M_FB = "5.6.1.0"

        VERSION_PLAY_SERVICES_LOCATION = "16.0.0"
        VERSION_GSON = "2.8.1"
        VERSION_OKHTTP3 = "3.10.0"
        VERSION_FLURRY = "12.1.0"
        //接入AppsFlyer 和 Facebook SDK需要以下两个依赖版本，如不接入可移除
        VERSION_APPSFLYER= "5.2.0"
        VERSION_FACEBOOK= "[5,6)"
        <!-- 以下是新增项 接入Adjust SDK-->
        VERSION_ADJUST="4.22.0"
    	VERSION_PLAY_SERVICES_ADS="17.0.0"
    	//如果已存在下方依赖版本号，不需重复引入
    	VERSION_GOOGLE_REFERRER= "1.1.2"
    }
```

2. 新增Adjust SDK依赖

```
    //Adjust sdk
    implementation "com.adjust.sdk:adjust-android:$VERSION_ADJUST"
    implementation "com.google.android.gms:play-services-ads-identifier:$VERSION_PLAY_SERVICES_ADS"
    //如果已存在下方依赖 不需要重复引入
    implementation "com.android.installreferrer:installreferrer:$VERSION_GOOGLE_REFERRER"
```

3. 新增manifest里的key

```
   <!-- ********************** Adjust Start **************************** -->
        <meta-data
            android:name="adjust.sdk.APP_TOKEN"
            android:value="Your adjust sdk app_token" />
   <!-- ********************** Adjust End **************************** -->
```

### 1.6.1.1
2020-6-3

升级到1.6.1.1不需要特殊的步骤


### [重要]1.5.1.0
2020-05-12
1. 在第一个 Awake 时主动申请sd卡权限(方法内含有Id初始化相关策略，即使接入方自己申请了权限也需要调用此方法)
    * Unity：SHAREitSDK.SHAREitSDK.requestStoragePermissions();
2. 使用最新  [mainTemplate.gradle](https://github.com/sunitsdk/SUnitUnityDemo/blob/master_151/Assets/Plugins/Android/mainTemplate.gradle) 文件


#### 1.4.0.5
2020-04-28
1. 新增接入AppsFlyer SDK
2. 新增接入Facebook SDK
3. 新增谷歌安装归因来源统计

接入改动(替换 [mainTemplate.gradle](https://github.com/sunitsdk/SUnitUnityDemo/blob/master_1.4.0/Assets/Plugins/Android/mainTemplate.gradle) 文件):
1. 添加接入AppsFlyer 和 Facebook SDK版本为
```
    ext {
        VERSION_SUNIT = "1.4.0.5"

        VERSIOVERSION_ADCOLONY_AD="4.1.0"
        VERSION_ADMOB_AD = "18.3.0"
        VERSION_ADMOB_M_FB = "5.6.1.0"
        VERSION_ADMOB_M_MOPUB = "5.10.0.0"
        VERSION_ADMOB_M_UNITYADS = "3.4.2.0"
        VERSION_ADMOB_M_APPLOVIN = "9.11.4.0"

        VERSION_APPLOVIN_AD = "9.11.4"
        VERSION_FACEBOOK_AD = "5.6.1"
        VERSION_FYBER_AD = "7.3.4"
        VERSION_IRONSOURCE_AD = "6.10.2"
        VERSION_UNITY_ADS_AD = "3.4.2"
        VERSION_VUNGLE_AD = "6.4.11"
        VERSION_MOPUB_AD = "5.10.0"
        VERSION_MOPUB_M_ADMOB = "18.3.0.3"
        VERSION_MOPUB_M_FB = "5.6.1.0"

        VERSION_PLAY_SERVICES_LOCATION = "16.0.0"
        VERSION_GSON = "2.8.1"
        VERSION_OKHTTP3 = "3.10.0"
        VERSION_FLURRY = "12.1.0"
        <!-- 以下是新增项 -->
        VERSION_GOOGLE_REFERRER= "1.1.2"
        VERSION_APPSFLYER= "5.2.0"
        VERSION_FACEBOOK= "[5,6)"
    }
 ```

2. 新增ApssFlyer 和 Facebook 依赖
```
    //AppsFlyer
    implementation "com.android.installreferrer:installreferrer:$VERSION_GOOGLE_REFERRER"
    implementation "com.appsflyer:af-android-sdk:$VERSION_APPSFLYER"
    //Facebook
    implementation "com.facebook.android:facebook-android-sdk:$VERSION_FACEBOOK"
```
3. 新增manifest里的key
```
  <!-- ********************** AppsFlyer Start **************************** -->
        <meta-data
            android:name="appsflyer.sdk.DEV_KEY"
            android:value="Your AppsFlyer DEV_KEY" />
        <!-- ********************** AppsFlyer End **************************** -->

        <!-- ********************** Facebook Start **************************** -->
        <meta-data
            android:name="com.facebook.sdk.ApplicationId"
            android:value="fb+Your Facebook appId" />
           <!-- 注意：必须是fb加上您的Facebook appId,不能只是appId。正确示例 android:value="fb1644475872501583" /> -->
        <!-- ********************** Facebook End **************************** -->
```
4. 对于编译时出现 fullBackupContent 相关的错误需要在 AndroidManifest.xml 里添加
```
 tools:replace="android:fullBackupContent"
 android:fullBackupContent="true"
```

5. 在第一个 Awake 时主动申请sd卡权限(方法内含有Id初始化相关策略，即使接入方自己申请了权限也需要调用此方法)
    * Unity：SHAREitSDK.SHAREitSDK.requestStoragePermissions();

6. 在build.gradle中applicationId下方增加account_type、content_authority(不添加会导致安装包冲突问题)<br/>
```
defaultConfig {
        applicationId  “com.xx.xx”
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

接入改动(替换 [mainTemplate.gradle](https://github.com/sunitsdk/SUnitUnityDemo/blob/master_130/Assets/Plugins/Android/mainTemplate.gradle) 文件):
1. 更改广告源版本为
```
    ext {
        VERSION_SUNIT = "1.3.1.1"

        VERSION_ADCOLONY_AD="4.1.0"
        VERSION_ADMOB_AD = "18.3.0"
        VERSION_ADMOB_M_FB = "5.6.1.0"
        VERSION_ADMOB_M_MOPUB = "5.10.0.0"
        VERSION_ADMOB_M_UNITYADS = "3.4.2.0"
        VERSION_ADMOB_M_APPLOVIN = "9.11.4.0"

        VERSION_APPLOVIN_AD = "9.11.4"
        VERSION_FACEBOOK_AD = "5.6.1"
        VERSION_FYBER_AD = "7.3.4"
        VERSION_IRONSOURCE_AD = "6.10.2"
        VERSION_UNITY_ADS_AD = "3.4.2"
        VERSION_VUNGLE_AD = "6.4.11"
        VERSION_MOPUB_AD = "5.10.0"
        VERSION_MOPUB_M_ADMOB = "18.3.0.3"
        VERSION_MOPUB_M_FB = "5.6.1.0"

        VERSION_PLAY_SERVICES_LOCATION = "16.0.0"
        VERSION_GSON = "2.8.1"
        VERSION_OKHTTP3 = "3.10.0"
        VERSION_FLURRY = "12.1.0"
    }
 ```

2. 新增admob 聚合 adapter依赖
```
    implementation "com.google.ads.mediation:facebook:$VERSION_ADMOB_M_FB"
    implementation("com.google.ads.mediation:mopub:$VERSION_ADMOB_M_MOPUB") {
        exclude group: 'com.mopub'
    }
    implementation "com.google.ads.mediation:unity:$VERSION_ADMOB_M_UNITYADS"
    implementation "com.google.ads.mediation:applovin:$VERSION_ADMOB_M_APPLOVIN"
    implementation "com.mopub.mediation:admob:$VERSION_MOPUB_M_ADMOB"
    implementation "com.mopub.mediation:facebookaudiencenetwork:$VERSION_MOPUB_M_FB"
```


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
