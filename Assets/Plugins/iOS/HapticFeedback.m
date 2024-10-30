// HapticFeedback.m

#import <UIKit/UIKit.h>

void PlayShortHaptic()
{
    UIImpactFeedbackGenerator *generator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleLight];
    [generator prepare];
    [generator impactOccurred];
}

void PlayLongHaptic()
{
    UIImpactFeedbackGenerator *generator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleHeavy];
    [generator prepare];
    [generator impactOccurred];
}
