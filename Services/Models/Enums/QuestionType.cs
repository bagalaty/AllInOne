namespace Services.Models.Enums
{
    public enum QuestionType
    {
        TRUE_FALSE = 1,
        MULTIPLE_CHOICES = 2,
        SINGLE_CHOICE = 4,
        COMPLETE = 8,
        //ESSAY = 5,
        //MATCHING = 6,
        //SEQUENCE = 7,
    }
    public enum QuestionDifficultyLevel
    {
        MEDIUM = 0, GOOD = 1, EXCELLENT = 2
    }

    public enum LanguageSkill
    {
        NOT_APPLICABLE = 0, LANGUAGE_OF_TEXT = 1, TEXT_UNDERSTANDING = 2, TEXT_ANALYSIS = 3
    }

    public enum QuestionSkill
    {
        REMEMBERING = 0, UNDERSTANDING = 1, APPLYING = 2, ANALYSING = 3, CREATING = 4, EVALUATING = 5
    }
}
