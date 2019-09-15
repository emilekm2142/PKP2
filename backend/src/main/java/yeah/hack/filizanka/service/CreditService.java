package yeah.hack.filizanka.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import yeah.hack.filizanka.model.Skin;
import yeah.hack.filizanka.model.User;
import yeah.hack.filizanka.repository.SkinRepository;
import yeah.hack.filizanka.repository.UserRepository;

@Service
@Transactional
@RequiredArgsConstructor
public class CreditService {

    private final UserRepository userRepository;

    private final SkinRepository skinRepository;

    public Skin exchangeCreditsForSkin(Long userId, Long skinId) {
        final Skin skin = skinRepository.getOne(skinId);
        final User user = userRepository.getOne(userId);

        user.setCredits(user.getCredits()-skin.getPrice());
        user.getSkins().add(skin);

        userRepository.saveAndFlush(user);

        return skin;
    }

}
