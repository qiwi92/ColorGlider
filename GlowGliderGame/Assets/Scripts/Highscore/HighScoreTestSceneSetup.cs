using System.Net;
using UnityEngine;

namespace Highscore
{
    public class HighScoreTestSceneSetup : MonoBehaviour
    {
        public HighScorePanelView PanelView;
        public HighScoreFaker HighScoreFaker;

        void Start () {

            ServicePointManager.ServerCertificateValidationCallback = ServerUtils.MyRemoteCertificateValidationCallback;

            var model = new HighScoreModel();
            PanelView.Initialize(model);
            HighScoreFaker.Initialize(PanelView);
            PanelView.PanelTransform.position = Vector3.zero;
        }
    }
}
