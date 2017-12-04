using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization.Words_Preporation.Words_Checker
{
    public class PartOfSpeechCheckerTest
    {
        [Test]
        public void POSChecker_ShouldSkipWords_LessThan3Letter()
        {
            var checker = new PartOfSpeechChecker(new Settings.Settings());

            var words = checker.GetSuitableWords(new[] {"the", "Alice"});
            words.Should().HaveCount(1);
        }
        [Test]
        public void POSChecker_ShouldSkip_Pronouns()
        {
            var checker = new PartOfSpeechChecker(new Settings.Settings());

            var words = checker.GetSuitableWords(new[] { "I","me", "door" });
            words.Should().HaveCount(1);
        }
        [Test]
        public void POSChecker_ShouldSkip_NotVerbs()
        {
            var settings = new Settings.Settings {SuitablePos = "vb"};
            var checker = new PartOfSpeechChecker(settings);
            
            var words = checker.GetSuitableWords(new[] { "play", "Alice" });
            words.Should().NotContain("Alice");
        }
    }
}