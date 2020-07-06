namespace yamlist.Modules.IO.Json.Model.Meta
{
    public class JobPlanAnchorCall : JobPlan
    {
        public AnchorCall AnchorCall { get; set; }

        public override string ToString()
        {
            return $"{AnchorCall}";
        }
    }
}