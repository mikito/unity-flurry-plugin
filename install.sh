#/bin/sh
PLUGIN_DIR=./unity-flurry/Assets/Plugins
GOOGLE_PLAY_SERVICES_DIR=$ANDROID_SDK_ROOT/extras/google/google_play_services/libproject/google-play-services_lib

# Android
cp $GOOGLE_PLAY_SERVICES_DIR/libs/google-play-services.jar $PLUGIN_DIR/Android/
mkdir -p $PLUGIN_DIR/Android/res/values/
cp $GOOGLE_PLAY_SERVICES_DIR/res/values/version.xml $PLUGIN_DIR/Android/res/values/

