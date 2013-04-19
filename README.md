Unity Flurry Plugin
===============

概要
-----
 * Unity iOS, AndroidでFlurryを使う 

使い方
--------
 * FlurryManagerプレハブを起動直後のシーンに追加
   * Flurry API KEY設定
   * 一応iPhoneとiPadのAPI Keyを切り分けられる。どちらか一方でもOK
 * イベントのログを取りたいところで以下呼び出し
   * FlurryManager.logEvent(string eventId);
   * FlurryManager.logEvent(string eventId, Hashtable parameters);
    
備考
--------
 * ロケーション情報取る場合はAndroidManifestに以下のいずれかを追加
   * android.permission.ACCESS_COARSE_LOCATION
   * android.permission.ACCESS_FINE_LOCATION
 * iOSロケーションは未実装
 * キャッチしてない例外やエラーを自動的に送る
 * Flurry SDK Version 
    * iOS : 4.1.0
    * Android : 3.1.0
 
