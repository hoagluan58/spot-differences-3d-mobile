using NFramework;
using UnityEngine;

namespace SpotDifferences
{
    public class TouchVFXController : SingletonMono<TouchVFXController>
    {
        [SerializeField] private ParticleSystem _touchRightVfxPf;
        [SerializeField] private ParticleSystem _touchWrongVfxPf;

        public void PlayRightVFX(Vector3 pos) => Instantiate(_touchRightVfxPf, pos, Quaternion.identity);

        public void PlayWrongVFX(Vector3 pos) => Instantiate(_touchWrongVfxPf, pos, Quaternion.identity);
    }
}
