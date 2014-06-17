#import "Flurry.h"

void flurryStartSession(char *appKey){
    [Flurry startSession:[NSString stringWithCString:appKey encoding:NSASCIIStringEncoding]];
}

void flurryLogEvent(char *eventName){
    [Flurry logEvent:[NSString stringWithCString:eventName encoding:NSASCIIStringEncoding]];
}

void flurryLogEventWithParameter(char *eventName, char **keys, char **values, int size){
    NSMutableDictionary *parameters = [[NSMutableDictionary alloc] init];
    for (int i = 0; i < size; i++) {
        [parameters setObject:[NSString stringWithCString:(char *)values[i] encoding:NSASCIIStringEncoding]
                        forKey:[NSString stringWithCString:(char *)keys[i] encoding:NSASCIIStringEncoding]];
    }

    [Flurry logEvent:[NSString stringWithCString:eventName encoding:NSASCIIStringEncoding]
               withParameters:parameters];
}

void flurrySetUserID(char* userId){
    [Flurry setUserID:[NSString stringWithCString:userId encoding:NSASCIIStringEncoding]];
}

void flurrySetAge(int age){
    [Flurry setAge:age];
}

void flurrySetGender(char* gender){
    [Flurry setGender:[NSString stringWithCString:gender encoding:NSASCIIStringEncoding]];
}

void flurryLogPageView(){
    [Flurry logPageView];
}

void flurryLogError(char *errorId, char *message){
    [Flurry logError:[NSString stringWithCString:errorId encoding:NSASCIIStringEncoding]
                      message:[NSString stringWithCString:message encoding:NSASCIIStringEncoding]
                    exception:NULL];
}

void flurrySetCrashReporting(BOOL enabled)
{
    [Flurry setCrashReportingEnabled: enabled];
}

bool flurryIsIpad(){
    if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPhone){
        return false;
	}else if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad){
        return true;
	}
    return false;
}