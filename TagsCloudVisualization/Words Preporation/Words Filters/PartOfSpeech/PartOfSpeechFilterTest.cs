    using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization.Words_Preporation.Words_Filters.PartOfSpeech
{
    public class PartOfSpeechFilterTest
    {
        [Test]
        public void POSChecker_ShouldSkipWords_LessThan3Letter()
        {
            var checker = new PartOfSpeechFilter(new Settings.Settings(), new PartOfSpeechGetter());

            var words = checker.GetSuitableWords(new[] { "the", "Alice" });
            words.Value.Should().HaveCount(1);
        }
        [Test]
        public void POSChecker_ShouldSkip_Pronouns()
        {
            var checker = new PartOfSpeechFilter(new Settings.Settings(), new PartOfSpeechGetter());

            var words = checker.GetSuitableWords(new[] { "I", "me", "door" });
            words.Value.Should().HaveCount(1); 
        }
        [Test]
        public void POSChecker_ShouldSkip_NotVerbs()
        {
            var settings = new Settings.Settings { SuitablePos = PartsOfSpeech.Verb };
            var checker = new PartOfSpeechFilter(settings, new PartOfSpeechGetter());

            var words = checker.GetSuitableWords(new[] { "play", "Alice" });
            words.Value.Should().NotContain("Alice");
        }
    }
}