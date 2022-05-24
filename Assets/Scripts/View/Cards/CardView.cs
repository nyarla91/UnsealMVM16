using System;
using Essentials;
using Model.Cards.Spells;
using TMPro;
using UnityEngine;

namespace View.Cards
{
    public class CardView : Transformer
    {
        private const float MaximizeSpeed = 15;
        
        [SerializeField] private MeshRenderer _icon;
        [SerializeField] private TextMeshPro _name;
        [SerializeField] private TextMeshPro _description;
        [SerializeField] private TextMeshPro _type;
        [SerializeField] private GameObject _charges;
        [SerializeField] private TextMeshPro _chargesValue;
        

        public Transform OffsetStandart { get; set; }
        private Vector3 TargetPositionOffset { get; set; }
        private Quaternion TargetRotationOffset { get; set; }

        public void Maximize()
        {
            TargetPositionOffset = OffsetStandart.localPosition;
            TargetRotationOffset = OffsetStandart.localRotation;
        }

        public void Minimize()
        {
            TargetPositionOffset = Vector3.zero;
            TargetRotationOffset = Quaternion.identity;
        }

        public void UpdateCharges(int charges)
        {
            _chargesValue.text = charges.ToString();
            _charges.SetActive(charges > 0);
        }

        public void UpadteView(Spell spell)
        {
            _icon.material = spell.Icon;
            _name.text = spell.Name.Localized;
            _description.text = FormatDescription(spell.Description.Localized);
            _type.text = spell.Type.ToString();
        }

        private void FixedUpdate()
        {
            MoveAndRotateTowardsTarget();
        }

        private void MoveAndRotateTowardsTarget()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, TargetPositionOffset, MaximizeSpeed * Time.fixedDeltaTime);
            transform.localRotation =
                Quaternion.Lerp(transform.localRotation, TargetRotationOffset, MaximizeSpeed * Time.fixedDeltaTime);
        }

        private string FormatDescription(string description)
        {
            description = description.Replace("[", "<b><color=#808000ff>");
            description = description.Replace("]", "</color></b>");
            description = description.Replace("<hp>", "<sprite index=0>");
            description = description.Replace("<dm>", "<sprite index=1>");
            description = description.Replace("<ap>", "<sprite index=2>");
            description = description.Replace("<bl>", "<sprite index=3>");
            description = description.Replace("<gr>", "<sprite index=4>");
            description = description.Replace("<ch>", "<sprite index=5>");
            description = description.Replace("<tx>", "<sprite index=6>");
            description = description.Replace("<cr>", "<sprite index=7>");
            return description;
        }
    }
}